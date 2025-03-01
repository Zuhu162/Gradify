using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentGradeTracker.Data;
using StudentGradeTracker.Helpers;
using StudentGradeTracker.Models;

namespace StudentGradeTracker.Controllers
{
    [Route("api/users")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {

        private readonly ApplicationDbContext _context;
        public UserController(ApplicationDbContext context, JwtHelper jwtHelper)
        {
            _context = context;
        }

        [HttpGet("students")]
        [Authorize(Roles = "Teacher")]
        public IActionResult GetStudents()
        {
            try
            {
                var userIdClaim = User.FindFirst("UserId")?.Value;
                if (string.IsNullOrEmpty(userIdClaim))
                    return Unauthorized("User ID not found in token.");
                var students = _context.Users
                .Where(u => EF.Property<string>(u, "Discriminator") == "Student")
                .Select(u => new
                {
                    u.Id,
                    u.Name
                }).ToList();
                return Ok(students);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
    }
}