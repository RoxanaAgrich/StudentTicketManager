using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using StudentTicketManagement.Api.Abstractions;
using static Application.Services.V1.Identity.Query;

namespace StudentTicketManagement.Api.Controllers.V1
{
    [ApiVersion(1)]
    public class AuthController : ApiController
    {
        public AuthController(ISender sender) : base(sender)
        {

        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Login login)
        {
            var result = await Sender.Send(login);

            if (result.IsFailure)
                return HandlerFailure(result);

            return Ok(result);
        }
    }
}
