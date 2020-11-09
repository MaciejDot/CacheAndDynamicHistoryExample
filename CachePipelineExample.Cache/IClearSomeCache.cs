using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CachePipelineExample.Cache
{
    public interface IClearSomeCache<CacheR, Response> where CacheR : ICacheResponse<Response>
    {
    }
}
