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

            // Check if student is allowed to submit
            var isStudentAssigned = _context.StudentAssignments
                .Any(sa => sa.AssignmentId == submission.AssignmentId && sa.UserId == userId);
            if (!isStudentAssigned)
                return Unauthorized("You are not assigned to this assignment.");

            // Create a new submission
            var newSubmission = new Submission
            {
                UserId = userId,
                AssignmentId = submission.AssignmentId,
                FileUrl = submission.FileUrl,
                SubmittedAt = DateTime.UtcNow,
                Status = "Pending",
                Grade = "Ungraded"
            };

            _context.Submissions.Add(newSubmission);
            _context.SaveChanges();

            return Ok("Assignment submitted successfully.");
        }
   

        //Get submissions for teachers
        [HttpGet("{assignmentId}/submissions")]
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
                    StudentName = _context.Users.FirstOrDefault(u => u.Id == s.UserId)?.Name // In-memory evaluation
                })
                .ToList();

            return Ok(submissions);
        }
    }
}
