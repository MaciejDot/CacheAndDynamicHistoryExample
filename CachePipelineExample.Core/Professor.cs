using System;
using System.Collections.Generic;
using System.Text;

namespace CachePipelineExample.Core
{
    public class Professor
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<CourseJoinProfessor> CourseJoinProfessor { get; set; }
    }
}
