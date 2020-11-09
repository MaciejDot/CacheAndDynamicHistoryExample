using CachePipelineExample.Cache;
using CachePipelineExampleContracts.Contracts.Student;
using CachePipelineExampleDomain.Domains.Query;
using MediatR;

namespace CachePipelineExampleDomain.Domains.Command
{
    public sealed class UpdateStudentCommand : UpdateStudentDTO, IRequest, IClearSomeCache<GetStudentQuery, StudentDTO>
    {
    }
}