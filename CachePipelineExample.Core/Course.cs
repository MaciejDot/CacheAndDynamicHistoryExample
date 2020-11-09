using System;
using System.Collections.Generic;
using System.Text;

namespace CachePipelineExample.Core
{
    public class Course
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<CourseJoinProfessor> CourseJoinProfessors { get; set; }
        public ICollection<CourseJoinStudent> CourseJoinStudents { get; set; }
    }
}
