using CachePipelineExample.Core;
using CachePipelineExampleDomain.Domains.Command;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CachePipelineExampleDomain.Domains.CommandHandler
{
    public sealed class CreateStudentCommandHandler : IRequestHandler<CreateStudentCommand, Unit>
    {
        private readonly ContextCore _context;
        public CreateStudentCommandHandler(ContextCore context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
        {
            await _context.Students.AddAsync(new Student
            {
                FirstName = request.FirstName,
                LastName = request.LastName
            }, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return new Unit();
        }
    }
}
