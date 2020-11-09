using CachePipelineExample.Core.Basics;
using System;
using System.Collections.Generic;
using System.Text;

namespace CachePipelineExample.Core
{
    public class HistoryEntity : BasicEntity, IHistory
    {
        public Guid HistoryId { get; set; }
    }
}
