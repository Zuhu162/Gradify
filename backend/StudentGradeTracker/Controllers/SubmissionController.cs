using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentGradeTracker.Data;
using StudentGradeTracker.Models;

namespace StudentGradeTracker.Controllers
{
    [Route("api/submissions")]
    [ApiController]
    [Authorize(Roles = "Student")]
    public class SubmissionController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SubmissionController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("submit")]
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
    }
}
