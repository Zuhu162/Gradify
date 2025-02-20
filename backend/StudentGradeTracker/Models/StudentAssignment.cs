namespace StudentGradeTracker.Models
{
    public class StudentAssignment
    {
        // Replace StudentId with UserId
        public int UserId { get; set; }  // Foreign Key (References Users table)
        public User User { get; set; }  // Navigation Property (Student)

        public int AssignmentId { get; set; }  // Foreign Key
        public Assignment Assignment { get; set; }  // Navigation Property
    }
}
