using System;
using System.Collections.Generic;
using System.Text;

namespace CachePipelineExample.Core
{
    public class CourseJoinProfessor
    {

        public Guid ProfessorId { get; set; }
        public Guid CourseId { get; set; }
        public Professor Professor { get; set; }
        public Course Course { get; set; }
    }
}
