using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentGradeTracker.Data;
using StudentGradeTracker.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace StudentGradeTracker.Controllers
{
    [Route("api/assignments")]
    [ApiController]
    [Authorize]

    public class AssignmentController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public AssignmentController(ApplicationDbContext context)
        {
            _context = context;
        }

        //Creating an assingment

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

        //Editing an assignment
        [HttpPatch("{assignmentId}/edit")]
        [Authorize(Roles = "Teacher")]
        public IActionResult EditAssignment(int assignmentId, [FromBody] Assignment updatedAssignment)
        {
            var userId = int.Parse(User.FindFirst("UserId")?.Value ?? "0"); // Get UserId from JWT Token

            var assignment = _context.Assignments.FirstOrDefault(a => a.Id == assignmentId && a.UserId == userId);

            if (assignment == null)
            {
                return NotFound("Assignment not found or you don't have permission to edit it.");
            }

            // Update assignment fields
            assignment.Name = updatedAssignment.Name;
            assignment.Instructions = updatedAssignment.Instructions;
            assignment.DueDate = updatedAssignment.DueDate;

            _context.SaveChanges(); // Save changes to the database

            return Ok(assignment);
        }

        //Deleting Assignments

        [HttpDelete("{assignmentId}")]
        [Authorize(Roles = "Teacher")]
        public IActionResult DeleteAssignment(int assignmentId)
        {
            // Get the authenticated teacher's UserId from JWT
            var userId = int.Parse(User.FindFirst("UserId")?.Value ?? "0");

            // Fetch the assignment
            var assignment = _context.Assignments.FirstOrDefault(a => a.Id == assignmentId && a.UserId == userId);

            if (assignment == null)
            {
                return NotFound("Assignment not found or you don't have permission to delete it.");
            }

            // delete all student assignments linked to this assignment
            var relatedStudentAssignments = _context.StudentAssignments.Where(sa => sa.AssignmentId == assignmentId);
            _context.StudentAssignments.RemoveRange(relatedStudentAssignments);

            // delete the assignment itself
            _context.Assignments.Remove(assignment);

            //  Save changes to the database
            _context.SaveChanges();

            return Ok("Assignment deleted successfully.");
        }

        //Adding Students to Assignments

        [HttpPatch("{assignmentId}/add-students")]
        [Authorize(Roles = "Teacher")]
        public IActionResult AddStudentsToAssignment(int assignmentId, [FromBody] List<int> studentIds)
        {
            // Get the authenticated teacher's UserId from JWT
            var userId = int.Parse(User.FindFirst("UserId")?.Value ?? "0");

            // Fetch the assignment
            var assignment = _context.Assignments.FirstOrDefault(a => a.Id == assignmentId && a.UserId == userId);
            if (assignment == null)
            {
                return NotFound("Assignment not found or you don't have permission to modify it.");
            }

            // Validate students
            var validStudents = _context.Users
                .Where(u => studentIds.Contains(u.Id) && EF.Property<string>(u, "Discriminator") == "Student")
                .ToList();

            if (!validStudents.Any())
            {
                return BadRequest("No valid students found. Please check the student IDs.");
            }

            // Add only new students
            foreach (var student in validStudents)
            {
                if (!_context.StudentAssignments.Any(sa => sa.AssignmentId == assignmentId && sa.UserId == student.Id))
                {
                    _context.StudentAssignments.Add(new StudentAssignment
                    {
                        UserId = student.Id,
                        AssignmentId = assignmentId
                    });
                }
            }

            _context.SaveChanges();
            return Ok("Students successfully added to the assignment.");
        }


        //Getting Assingments

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
                                        .Where(a => a.UserId == userId)
                                        .OrderByDescending(a => a.DueDate)
                                        .Select(a => new
                                        {
                                            a.Id,
                                            a.Name,
                                            a.DueDate,
                                            StudentCount = a.StudentAssignments.Count // gets the number of students
                                        })
                                        .ToList();

                return Ok(assignments);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        //Remove students
        [HttpPatch("{assignmentId}/remove-student/{studentId}")]
        [Authorize(Roles = "Teacher")]
        public IActionResult RemoveStudentFromAssignment(int assignmentId, int studentId)
        {
            // Get the authenticated teacher's UserId from JWT
            var userId = int.Parse(User.FindFirst("UserId")?.Value ?? "0");

            // Fetch the assignment
            var assignment = _context.Assignments.FirstOrDefault(a => a.Id == assignmentId && a.UserId == userId);
            if (assignment == null)
            {
                return NotFound("Assignment not found or you don't have permission to modify it.");
            }

            // Find the student in the assignment
            var studentAssignment = _context.StudentAssignments
                .FirstOrDefault(sa => sa.AssignmentId == assignmentId && sa.UserId == studentId);

            if (studentAssignment == null)
            {
                return BadRequest("Student is not part of this assignment.");
            }

            // Remove the student from the assignment
            _context.StudentAssignments.Remove(studentAssignment);
            _context.SaveChanges();

            return Ok($"Student {studentId} successfully removed from the assignment.");
        }



        //Get Individal Assignment
        [HttpGet("{assignmentId}")]
        [Authorize(Roles = "Teacher")]
        public IActionResult GetAssignment(int assignmentId)
        {
            var userIdClaim = User.FindFirst("UserId")?.Value;
            if (string.IsNullOrEmpty(userIdClaim))
                return Unauthorized("User ID not found in token.");
            int userId = int.Parse(userIdClaim);

            if (string.IsNullOrEmpty(userIdClaim)) return Unauthorized("User ID not found in token.");
            var assignment = _context.Assignments
            .Where(a => a.UserId == userId && a.Id == assignmentId)
            .Select(a => new
            {
                a.Id,
                a.Name,
                a.DueDate,
                a.Instructions,
                StudentCount = a.StudentAssignments.Count, // Include student count
                a.StudentAssignments
            })
            .FirstOrDefault();
            if (assignment == null)
            {
                return NotFound("Assignment not found or you don't have permission to view it.");
            }

            return Ok(assignment);
        }


        //Get Assignment for Students
        [HttpGet("student-assignments")]
        [Authorize(Roles = "Student")]
        public IActionResult GetStudentAssignments()
        {
            try
            {
                var userIdClaim = User.FindFirst("UserId")?.Value;
                if (string.IsNullOrEmpty(userIdClaim)) return Unauthorized("User ID not found in token.");

                int userId = int.Parse(userIdClaim);

                var assignments = _context.StudentAssignments.
                Where(sa => sa.UserId == userId).
                Select(sa => sa.Assignment).
                ToList();

                return Ok(assignments);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server Error: {ex.Message}");
            }
        }
    }
}