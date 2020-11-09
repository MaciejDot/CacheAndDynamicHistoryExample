using System;
using System.Collections.Generic;
using System.Text;

namespace CachePipelineExample.Core
{
    public class Student
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<CourseJoinStudent> CourseJoinStudents { get; set; }
    }
}
