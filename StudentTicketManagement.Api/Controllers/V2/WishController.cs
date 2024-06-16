using Application.Services.V2.Wish;
using Asp.Versioning;
using Domain.Extensions;
using Domain.Shared;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using StudentTicketManagement.Api.Abstractions;

namespace StudentTicketManagement.Api.Controllers.V2
{
    [ApiVersion(2)]
    public class WishController : ApiController
    {
        public WishController(ISender sender) : base(sender)
        {
        }

        [HttpPost(Name = "CreateWish")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CreateWish([FromBody] Command.CreateWishCommand createWish)
        {
            var result = await Sender.Send(createWish);

            if (result.IsFailure)
                return HandlerFailure(result);

            return Ok(result);
        }

        [HttpGet(Name = "GetWishes")]
        [ProducesResponseType(typeof(Result<IEnumerable<Response.WishResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetWishes(string? serchTerm = null,
            string? sortColumn = null,
            string? sortOrder = null,
            string? sortColumnAndOrder = null,
            int pageIndex = 1,
            int pageSize = 10)
        {
            var result = await Sender.Send(new Query.GetWishesQuery(serchTerm, sortColumn,
                SortOrderExtension.ConvertStringToSortOrder(sortOrder),
                SortOrderExtension.ConvertStringToSortOrderV2(sortColumnAndOrder),
                pageIndex,
                pageSize));
            return Ok(result);
        }

        [HttpGet("{wishId}")]
        [ProducesResponseType(typeof(Result<Response.WishResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetWishById(Guid wishId)
        {
            var result = await Sender.Send(new Query.GetWishByIdQuery(wishId));
            return Ok(result);
        }

        [HttpDelete("{wishId}")]
        [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteWishById(Guid wishId)
        {
            var result = await Sender.Send(new Command.DeleteWishCommand(wishId));
            return Ok(result);
        }

        [HttpPut("{wishId}")]
        [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateWishById(Guid wishId, [FromBody] Command.UpdateWishCommand updateWish)
        {
            var updateWishCommand = new Command.UpdateWishCommand(wishId, updateWish.name);
            var result = await Sender.Send(updateWishCommand);
            return Ok(result);
        }
    }
}
