using System;

namespace StudentGradeTracker.Models{
    public class Submission{
        public int Id { get; set; }
        public int UserId { get; set; } // Replaced StudentId with UserId
        public User? User { get; set; }  // Navigation Property

        public int AssignmentId { get; set; }  // Foreign Key
        public Assignment? Assignment { get; set; }  // Navigation Property

        public string FileUrl { get; set; } = string.Empty;  // Submission link
        public DateTime SubmittedAt { get; set; } = DateTime.UtcNow;  // Timestamp
        public string Grade { get; set; } = "Ungraded";
        public string Status { get; set; } = "Pending";
    }
}