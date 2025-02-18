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
        private string PasswordHash { get; set; } = string.Empty;  // Hidden password

        public void SetPassword(string password)
        {
            PasswordHash = HashPassword(password);  // Store hashed password
        }

        private string HashPassword(string password)
        {
            // Simple hashing (Replace with BCrypt in production)
            return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(password));
        }
    }
}
