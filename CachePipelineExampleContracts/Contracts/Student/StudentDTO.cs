using System;
using System.Collections.Generic;
using System.Text;

namespace CachePipelineExampleContracts.Contracts.Student
{
    public class StudentDTO
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
