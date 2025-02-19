using Microsoft.AspNetCore.Mvc;
using StudentGradeTracker.Models;
using StudentGradeTracker.Data;
using StudentGradeTracker.Helpers;
using Microsoft.EntityFrameworkCore;

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
            if (userType.ToLower() != "student" && userType.ToLower() != "teacher")
                return BadRequest("Invalid user type. Must be 'student' or 'teacher'.");

            var existingUser = _context.Users.FirstOrDefault(u => u.Email == request.Email);
            if (existingUser != null)
                return BadRequest("Email already exists.");

            User newUser;
            if (userType.ToLower() == "student")
                newUser = new Student { Name = request.Name, Email = request.Email };
            else
                newUser = new Teacher { Name = request.Name, Email = request.Email };

            newUser.SetPassword(request.Password);
            _context.Users.Add(newUser);
            _context.SaveChanges();

            return Ok($"{userType} registered successfully.");
        }

        [HttpPost("login/{userType}")]
        public IActionResult Login(string userType, [FromBody] LoginRequest request)
        {
            if (userType.ToLower() != "student" && userType.ToLower() != "teacher")
                return BadRequest("Invalid user type. Must be 'student' or 'teacher'.");

            User? user = userType.ToLower() == "student"
                ? _context.Students.FirstOrDefault(u => u.Email == request.Email)
                : _context.Teachers.FirstOrDefault(u => u.Email == request.Email);

            if (user == null || !user.VerifyPassword(request.Password))
            {
                return Unauthorized("Invalid email or password.");
            }

            var token = _jwtHelper.GenerateToken(user.Email, userType);
            return Ok(new { Token = token });
        }
    }

    public class RegisterRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
