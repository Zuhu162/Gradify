using System;
using System.Collections.Generic;

namespace StudentGradeTracker.Models{
    public class Assignment{
        public int Id {get; set;}
        public string Name {get; set;} = string.Empty;
        public string Instructions {get; set;} = string.Empty;

        public DateTime DueDate {get; set;}
        public int TeacherId {get; set;}
        public Teacher Teacher {get; set;} 

        public List<StudentAssignment> StudentAssignments {get; set;} = new List<StudentAssignment>();

    }
}