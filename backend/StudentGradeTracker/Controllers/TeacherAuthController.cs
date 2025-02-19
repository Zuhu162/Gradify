using Microsoft.AspNetCore.Mvc;
using StudentGradeTracker.Models;
using StudentGradeTracker.Data;

namespace StudentGradeTracker.Controllers
{
    [Route("api/auth/teacher")]
    [ApiController]
    public class TeacherAuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TeacherAuthController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterRequest request)
        {
            // Check if the email is already taken
            var existingUser = _context.Users.FirstOrDefault(u => u.Email == request.Email);
            if (existingUser != null)
                return BadRequest("Email already exists.");

            // Create new teacher instance
            var teacher = new Teacher
            {
                Name = request.Name,
                Email = request.Email
            };

            // Hash the password
            teacher.SetPassword(request.Password);

            _context.Teachers.Add(teacher);
            _context.SaveChanges();

            return Ok("Teacher registered successfully.");
        }
    }

    public class RegisterRequest
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
