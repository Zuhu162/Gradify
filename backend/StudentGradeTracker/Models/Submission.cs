using System;

namespace StudentGradeTracker.Models{
    public class Submission{
        public int Id {get; set;}
        public int StudentId {get; set;} // Foreign Key - Student who submitted
        public Student? Student { get; set; }  // Navigation Property
        public int AssignmentId { get; set; }  // Foreign Key - Assignment being submitted
        public Assignment? Assignment { get; set; }  // Navigation Property

        public string FileUrl { get; set; } = string.Empty;  // Link to submission (PDF or Google Doc)

        public DateTime SubmittedAt { get; set; } = DateTime.UtcNow;  // Timestamp of submission

        public string Grade { get; set; } = "Ungraded";  // Default to "Ungraded"

        public string Status { get; set; } = "Pending";  // Status of the submission (Pending, Graded, etc.)
    }
}