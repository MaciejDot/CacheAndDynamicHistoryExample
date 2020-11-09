using System;
using System.Collections.Generic;
using System.Text;

namespace CachePipelineExample.Core
{
    interface IHistory
    {
        public Guid HistoryId { get; set; }
    }
}
