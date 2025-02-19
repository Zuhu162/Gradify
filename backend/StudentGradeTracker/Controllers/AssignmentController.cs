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
        public IActionResult CreateAssignment([FromBody] Assignment assignment){
            _context.Assignments.Add(assignment);
            _context.SaveChanges();
            return Ok(assignment);
        }

        [HttpGet]
        [Authorize(Roles = "Teacher")]

        // Get the authenticated teacher's ID
        public IActionResult GetAssignments(){
            var teacherId = int.Parse(User.FindFirst("id")?.Value ?? "0");

             // Fetch only the assignments created by this teacher
            var assignments = _context.Assignments.Where(a => a.TeacherId == teacherId).ToList();
            return Ok(assignments);
        }
    }
}