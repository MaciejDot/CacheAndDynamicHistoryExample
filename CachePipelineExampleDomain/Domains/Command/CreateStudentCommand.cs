using CachePipelineExample.Cache;
using CachePipelineExampleContracts.Contracts.Student;
using MediatR;

namespace CachePipelineExampleDomain.Domains.Command
{
    public sealed class CreateStudentCommand : CreateStudentDTO, IClearAllCache, IRequest
    {
    }
}