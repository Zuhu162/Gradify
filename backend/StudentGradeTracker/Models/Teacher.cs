using System.Collections.Generic;

namespace StudentGradeTracker.Models
{
    public class Teacher : User  // Inherits from User
    {
        public List<Assignment> Assignments { get; set; } = new List<Assignment>();  // Assignments created by the teacher
    }
}
