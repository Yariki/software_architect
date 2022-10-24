using CartingService.Application.Cart.Commands.AddCartItem;
using CartingService.Application.Cart.Commands.RemoveCartItem;
using CartingService.Application.Cart.Models;
using CartingService.Application.Cart.Queries.GetCart;

using Microsoft.AspNetCore.Mvc;

namespace CartingService.Controllers.v1
{
    [ApiController]
    [Route("api/v{version:apiVersion}/cart")]
    [ApiVersion("1.0")]
    public class CartController : ApiControllerBase
    {
        [MapToApiVersion("1.0")]
        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CartDto>> GetCart([FromRoute] GetCartQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpPost]
        [Route("add")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CartDto>> AddItem([FromBody] AddItemCommand cmd)
        {
            return Ok(await Mediator.Send(cmd));
        }

        [HttpDelete]
        [Route("remove")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CartDto>> RemoveItem([FromBody] RemoveCartItemCommand cmd)
        {
            return Ok(await Mediator.Send(cmd));
        }

    }
}
