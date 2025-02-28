using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentGradeTracker.Data;
using StudentGradeTracker.Models;

namespace StudentGradeTracker.Controllers
{
    [Route("api/submissions")]
    [ApiController]
    public class SubmissionController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SubmissionController(ApplicationDbContext context)
        {
            _context = context;
        }

        //Submit assignments for students    
        [HttpPost("submit")]
        [Authorize(Roles = "Student")]
        public IActionResult SubmitAssignment([FromBody] Submission submission)
        {
            // Extract UserId from JWT token
            var userId = int.Parse(User.FindFirst("UserId")?.Value ?? "0");

            // Validate assignment exists
            var assignment = _context.Assignments.FirstOrDefault(a => a.Id == submission.AssignmentId);
            if (assignment == null)
                return NotFound("Assignment not found.");

            // ✅ Check if the deadline has passed
            if (DateTime.UtcNow > assignment.DueDate)
                return BadRequest("The deadline for this assignment has passed. Submission not allowed.");

            // Check if student is allowed to submit
            var isStudentAssigned = _context.StudentAssignments
                .Any(sa => sa.AssignmentId == submission.AssignmentId && sa.UserId == userId);
            if (!isStudentAssigned)
                return Unauthorized("You are not assigned to this assignment.");

            // ✅ Check for existing submission
            var existingSubmission = _context.Submissions
                .FirstOrDefault(s => s.AssignmentId == submission.AssignmentId && s.UserId == userId);
            if (existingSubmission != null)
                return BadRequest("You have already submitted this assignment.");

            // Create a new submission
            var newSubmission = new Submission
            {
                UserId = userId,
                AssignmentId = submission.AssignmentId,
                FileUrl = submission.FileUrl,
                SubmittedAt = DateTime.UtcNow,
                Status = "Submitted",
                Grade = "Ungraded"
            };

            _context.Submissions.Add(newSubmission);
            _context.SaveChanges();

            return Ok("Assignment submitted successfully.");
        }


        //Get all submissions 
        [HttpGet("my-submissions")]
        [Authorize(Roles = "Student")]
        public IActionResult GetMySubmissions()
        {
            // Extract UserId from JWT token
            var userId = int.Parse(User.FindFirst("UserId")?.Value ?? "0");

            // Fetch all submissions by the student
            var submissions = _context.Submissions
                .Where(s => s.UserId == userId)
                .ToList()
                .Select(s => new
                {
                    s.Id,
                    AssignmentName = _context.Assignments.FirstOrDefault(a => a.Id == s.AssignmentId)?.Name,
                    s.FileUrl,
                    s.SubmittedAt,
                    s.Grade,
                    s.Status
                });

            return Ok(submissions);
        }

        // Get a student's own submission for a specific assignment
        [HttpGet("{assignmentId}/my-submission")]
        [Authorize(Roles = "Student")]
        public IActionResult GetMySubmission(int assignmentId)
        {
            // Extract UserId from JWT token
            var userId = int.Parse(User.FindFirst("UserId")?.Value ?? "0");

            // Fetch the student's submission for the given assignment
            var submission = _context.Submissions
                .FirstOrDefault(s => s.AssignmentId == assignmentId && s.UserId == userId);

            if (submission == null)
            {
                return NotFound("No submission found for this assignment.");
            }

            // Return the submission details
            return Ok(new
            {
                submission.Id,
                submission.FileUrl,
                submission.SubmittedAt,
                submission.Grade,
                submission.Status
            });
        }

        // Delete a submission by ID
        [HttpDelete("{submissionId}/delete")]
        [Authorize(Roles = "Student")]
        public IActionResult DeleteSubmission(int submissionId)
        {
            // Extract UserId from JWT token
            var userId = int.Parse(User.FindFirst("UserId")?.Value ?? "0");

            // Find the submission and validate ownership
            var submission = _context.Submissions.FirstOrDefault(s => s.Id == submissionId && s.UserId == userId);

            if (submission == null)
                return NotFound("Submission not found or you don't have permission to delete it.");

            // Prevent deletion if the submission is already graded
            if (submission.Status != "Pending")
                return BadRequest("You cannot delete a submission that has already been graded.");

            // Delete the submission
            _context.Submissions.Remove(submission);
            _context.SaveChanges();

            return Ok("Submission deleted successfully.");
        }

        //Get submissions for teachers
        [HttpGet("{assignmentId}")]
        [Authorize(Roles = "Teacher")]
        public IActionResult GetSubmissionsForAssignment(int assignmentId)
        {
            // Extract UserId from JWT
            var userId = int.Parse(User.FindFirst("UserId")?.Value ?? "0");

            // Verify if the assignment belongs to the authenticated teacher
            var assignment = _context.Assignments.FirstOrDefault(a => a.Id == assignmentId && a.UserId == userId);
            if (assignment == null)
                return NotFound("Assignment not found or you don't have permission to view submissions.");

            // Fetch all submissions for this assignment
            var submissions = _context.Submissions
                .Where(s => s.AssignmentId == assignmentId)
                .ToList() // Fetch from database first
                .Select(s => new
                {
                    s.Id,
                    s.FileUrl,
                    s.SubmittedAt,
                    s.Grade,
                    s.Status,
                    StudentName = _context.Users.FirstOrDefault(u => u.Id == s.UserId)?.Name, // In-memory evaluation
                    StudentId = _context.Users.FirstOrDefault(u => u.Id == s.UserId)?.Id // In-memory evaluation
                })
                .ToList();

            return Ok(submissions);
        }

        // Grade a specific submission
        [HttpPatch("{submissionId}/grade")]
        [Authorize(Roles = "Teacher")]
        public IActionResult GradeSubmission(int submissionId, [FromBody] GradeRequest request)
        {
            // Extract UserId from JWT token
            var userId = int.Parse(User.FindFirst("UserId")?.Value ?? "0");

            // Fetch the submission and verify the teacher owns the related assignment
            var submission = _context.Submissions
                .FirstOrDefault(s => s.Id == submissionId && s.Assignment.UserId == userId);

            if (submission == null)
            {
                return NotFound("Submission not found or you don't have permission to grade this submission.");
            }

            // Update grade and status
            submission.Grade = request.Grade;
            submission.Status = "Graded";

            _context.Submissions.Update(submission);
            _context.SaveChanges();

            return Ok("Submission graded successfully.");
        }

        // Request model for grading
        public class GradeRequest
        {
            public string Grade { get; set; } = string.Empty;
        }


    }
}
