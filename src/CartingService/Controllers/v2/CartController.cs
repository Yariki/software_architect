using CartingService.Application.Cart.Commands.AddCartItem;
using CartingService.Application.Cart.Commands.RemoveCartItem;
using CartingService.Application.Cart.Models;
using CartingService.Application.Cart.Queries.GetCart;
using Microsoft.AspNetCore.Mvc;

namespace CartingService.Controllers.v2;

[ApiController]
[Route("api/v{version:apiVersion}/cart")]
[ApiVersion("2.0")]
public class CartController : ApiControllerBase
{
    [MapToApiVersion("2.0")]
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<CartItemDto>>> GetCart([FromRoute] Guid id)
    {
        
        return Ok(await Mediator.Send(new GetCartQueryV2(){Id = id}));
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