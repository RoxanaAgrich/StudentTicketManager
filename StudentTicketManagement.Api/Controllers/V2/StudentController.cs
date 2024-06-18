using Application.Services.V2.Student;
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using StudentTicketManagement.Api.Abstractions;

namespace StudentTicketManagement.Api.Controllers.V2
{
    [ApiVersion(2)]
    public class StudentController: ApiController
    {
        public StudentController(ISender sender) :base (sender) { }
        [HttpPost (Name = "CreateStudent")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateStudent([FromBody] Command.CreateStudentCommand createStudent,CancellationToken cancellationToken)
        {
            var result  = await Sender.Send(createStudent,cancellationToken);
            if (result.IsFailure)
                return HandlerFailure(result);
            return Ok(result);
        }

    }
}
