namespace StudentGradeTracker.Models
{
    public class StudentAssignment
    {
        public int StudentId { get; set; }  // Foreign Key
        public Student Student { get; set; }  // Navigation Property

        public int AssignmentId { get; set; }  // Foreign Key
        public Assignment Assignment { get; set; }  // Navigation Property
    }
}
