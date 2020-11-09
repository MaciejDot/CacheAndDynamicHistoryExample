using System;
using System.Collections.Generic;
using System.Text;

namespace CachePipelineExample.Core.Basics
{
    public class BasicEntity : BaseEntity
    {
        public Guid Id {get; set;}
        public string Field { get; set; }
    }
}
