using Microsoft.AspNetCore.Mvc;
using StudentGradeTracker.Models;
using StudentGradeTracker.Data;
using Microsoft.AspNetCore.Identity.Data;

namespace StudentGradeTracker.Controllers
{
    [Route("api/auth/student")]
    [ApiController]
    public class StudentAuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public StudentAuthController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterRequest request)
        {
            // Check if email is already taken
            var existingUser = _context.Users.FirstOrDefault(u => u.Email == request.Email);
            if (existingUser != null)
                return BadRequest("Email already exists.");

            // Create new student instance
            var student = new Student
            {
                Name = request.Name,
                Email = request.Email
            };

            // Hash the password
            student.SetPassword(request.Password);

            _context.Students.Add(student);
            _context.SaveChanges();

            return Ok("Student registered successfully.");
        }

        // Define a request model for registration
        public class RegisterRequest
        {
            public string Name { get; set; } = string.Empty;
            public string Email { get; set; } = string.Empty;
            public string Password { get; set; } = string.Empty;
        }


        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request){
            var student = _context.Students.FirstOrDefault(u => u.Email == request.Email);
            if (student == null){
                return Unauthorized("Invalid email or password.");
            }

            // Verify password using BCrypt
            if (!student.VerifyPassword(request.Password)){
                return Unauthorized("Invalid email or password.");
            }

            return Ok("Login successful");
        }
    }
}
