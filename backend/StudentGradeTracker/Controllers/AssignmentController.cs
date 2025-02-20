using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentGradeTracker.Data;
using StudentGradeTracker.Models;

namespace StudentGradeTracker.Controllers{
    [Route("api/assignments")]
    [ApiController]
    [Authorize]
    
    public class AssignmentController : ControllerBase {
        private readonly ApplicationDbContext _context;
        public AssignmentController(ApplicationDbContext context){
            _context = context;
        }

        [HttpPost("create")]
        [Authorize(Roles = "Teacher")]
        public IActionResult CreateAssignment([FromBody] Assignment assignment)
        {
            var userId = int.Parse(User.FindFirst("UserId")?.Value ?? "0");  // Get UserId from JWT Token
            assignment.UserId = userId; // Assign the authenticated user's ID

            _context.Assignments.Add(assignment);
            _context.SaveChanges();
            return Ok(assignment);
        }

        [HttpGet]
        [Authorize(Roles = "Teacher")]
        public IActionResult GetAssignments()
        {
            try
            {
                // Extract the UserId from the JWT token
                var userIdClaim = User.FindFirst("UserId")?.Value;
                if (string.IsNullOrEmpty(userIdClaim))
                    return Unauthorized("User ID not found in token.");

                int userId = int.Parse(userIdClaim); // Convert UserId from string to int

                // Fetch assignments created by the logged-in teacher
                var assignments = _context.Assignments
                                        .Where(a => a.UserId == userId) // Use UserId instead of TeacherId
                                        .ToList();

                return Ok(assignments);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
    }
}