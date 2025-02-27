using Microsoft.AspNetCore.Mvc;
using StudentGradeTracker.Models;
using StudentGradeTracker.Data;
using StudentGradeTracker.Helpers;

namespace StudentGradeTracker.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly JwtHelper _jwtHelper;

        public AuthController(ApplicationDbContext context, JwtHelper jwtHelper)
        {
            _context = context;
            _jwtHelper = jwtHelper;
        }

        [HttpPost("register/{userType}")]
        public IActionResult Register(string userType, [FromBody] RegisterRequest request)
        {
            // Using Enum to validate user types
            if (!Enum.TryParse(userType, true, out UserRole role))
                return BadRequest("Invalid user type.");

            var existingUser = _context.Users.FirstOrDefault(u => u.Email == request.Email);
            if (existingUser != null)
                return BadRequest("Email already exists.");

            User newUser = role switch
            {
                UserRole.Student => new Student { Name = request.Name, Email = request.Email },
                UserRole.Teacher => new Teacher { Name = request.Name, Email = request.Email },
                _ => throw new ArgumentException("Invalid role")  // Future-proofing
            };

            newUser.SetPassword(request.Password);
            _context.Users.Add(newUser);
            _context.SaveChanges();

            return Ok($"{role} registered successfully.");
        }

        [HttpPost("login/{userType}")]
        public IActionResult Login(string userType, [FromBody] LoginRequest request)
        {
            if (userType.ToLower() != "student" && userType.ToLower() != "teacher")
                return BadRequest("Invalid user type. Must be 'student' or 'teacher'.");

            User? user = _context.Users.FirstOrDefault(u => u.Email == request.Email);

            if (user == null || !user.VerifyPassword(request.Password))
            {
                return Unauthorized("Invalid email or password.");
            }

            var token = _jwtHelper.GenerateToken(user.Id, user.Name, user.Email, userType);
            return Ok(new { Token = token });
        }
    }
    public class RegisterRequest
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    public class LoginRequest
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
