﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CachePipelineExample.Core
{
    public class CourseJoinStudent
    {
        public Guid StudentId { get; set; }
        public Guid CourseId { get; set; }
        public Course Course { get; set; }
        public Student Student { get; set; }
    }
}
