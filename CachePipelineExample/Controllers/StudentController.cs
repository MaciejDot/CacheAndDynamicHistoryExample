using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CachePipelineExampleContracts.Contracts.Student;
using CachePipelineExampleDomain.Domains.Command;
using CachePipelineExampleDomain.Domains.Query;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CachePipelineExample.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IMediator _mediator;
        public StudentController(IMediator mediator) 
        {
            _mediator = mediator;
        }
        [HttpGet]
        public Task<StudentDTO> Get([FromQuery]GetStudentQuery query, CancellationToken cancellationToken) 
        {
            return _mediator.Send(query, cancellationToken);
        }
        [HttpPost]
        public Task Post([FromBody]CreateStudentCommand command, CancellationToken cancellationToken) 
        {
            return _mediator.Send(command, cancellationToken);
        }
        [HttpPut]
        public Task Put([FromBody] UpdateStudentCommand command, CancellationToken cancellationToken) 
        {
            return _mediator.Send(command, cancellationToken);
        }
    }
}
