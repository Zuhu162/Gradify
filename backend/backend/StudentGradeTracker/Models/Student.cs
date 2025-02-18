using System.Collections.Generic;

namespace StudentGradeTracker.Models{
    public class Student: User{
        public List<StudentAssignment> StudentAssignments { get; set; } = new List<StudentAssignment>();
    }
}