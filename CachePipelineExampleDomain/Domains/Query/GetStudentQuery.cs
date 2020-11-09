using CachePipelineExample.Cache;
using CachePipelineExampleContracts.Contracts.Student;

namespace CachePipelineExampleDomain.Domains.Query
{
    public sealed class GetStudentQuery : GetStudentDTO, ICacheResponse<StudentDTO>
    {
    }
}