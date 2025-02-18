using System.Collections.Generic;

namespace StudentGradeTracker.Models
{
    public class Teacher : User  // Inherits from User
    {
        public List<string> Subjects { get; set; } = new List<string>();  // List of subjects a teacher teaches

        public List<Assignment> Assignments { get; set; } = new List<Assignment>();  // Assignments created by the teacher
    }
}
