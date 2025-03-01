using System.ComponentModel.DataAnnotations;

namespace StudentGradeTracker.Models
{
    public abstract class User  // Abstract class to prevent direct instantiation
    {
        [Key]
        public int Id { get; set; }  // Primary Key

        [Required, MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string PasswordHash { get; set; } = string.Empty;  // Hidden password

        public void SetPassword(string password)
        {
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool VerifyPassword(string password)
        {
            return BCrypt.Net.BCrypt.Verify(password, PasswordHash);
        }

        // Navigation properties
        public List<StudentAssignment> StudentAssignments { get; set; } = new List<StudentAssignment>();
        public List<Assignment> Assignments { get; set; } = new List<Assignment>(); // For teachers
    }
}
