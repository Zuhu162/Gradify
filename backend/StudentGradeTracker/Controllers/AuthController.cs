using Microsoft.AspNetCore.Mvc;
using StudentGradeTracker.Models;
using BCrypt.Net;
using StudentGradeTracker.Data;

namespace StudentGradeTracker.Controllers{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase{
        private readonly ApplicationDbContext _context;

        public AuthController(ApplicationDbContext context){
            _context = context;
        }
    }
}