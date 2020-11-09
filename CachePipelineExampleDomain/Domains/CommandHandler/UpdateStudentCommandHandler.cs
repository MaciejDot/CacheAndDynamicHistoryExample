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
    public sealed class UpdateStudentCommandHandler : IRequestHandler<UpdateStudentCommand, Unit>
    {
        private readonly ContextCore _context;
        public UpdateStudentCommandHandler(ContextCore context) 
        {
            _context = context;
        }
        public async Task<Unit> Handle(UpdateStudentCommand request, CancellationToken cancellationToken)
        {
            var student = await _context.Students.FindAsync(new object[] { request.Id }, cancellationToken);
            student.FirstName = request.FirstName;
            student.LastName = request.LastName;
            await _context.SaveChangesAsync(cancellationToken);
            return new Unit();
        }
    }
}
