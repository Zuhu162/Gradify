using System;
using System.Collections.Generic;

namespace StudentGradeTracker.Models
{
    public class Assignment
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Instructions { get; set; } = string.Empty;

        public DateTime DueDate { get; set; }

        public int UserId { get; set; }  // Foreign Key (References Users table)
        public User? User { get; set; }  // Navigation Property (Teacher)

        public List<StudentAssignment> StudentAssignments { get; set; } = new List<StudentAssignment>();
    }
}
