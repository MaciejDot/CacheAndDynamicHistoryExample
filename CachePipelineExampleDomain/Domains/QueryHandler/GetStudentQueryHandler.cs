using CachePipelineExample.Core;
using CachePipelineExampleContracts.Contracts.Student;
using CachePipelineExampleDomain.Domains.Query;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CachePipelineExampleDomain.Domains.QueryHandler
{
    public sealed class GetStudentQueryHandler : IRequestHandler<GetStudentQuery, StudentDTO>
    {
        private readonly ContextCore _context;
        public GetStudentQueryHandler(ContextCore context) 
        {
            _context = context;
        }
        public Task<StudentDTO> Handle(GetStudentQuery request, CancellationToken cancellationToken)
        {
            return _context.Students.Select(x => new StudentDTO { FirstName = x.FirstName, LastName = x.LastName, Id = x.Id }).SingleAsync(x => x.Id == request.Id, cancellationToken);
        }
    }
}
