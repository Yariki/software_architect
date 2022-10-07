using CartingService.Application.Cart.Commands.AddCartItem;
using CartingService.Application.Cart.Commands.RemoveCartItem;
using CartingService.Application.Cart.Queries.GetCart;

using Microsoft.AspNetCore.Mvc;

namespace CartingService.Controllers
{
    [ApiController]
    [Route("cart")]
    public class CartController : ApiControllerBase
    {
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<CartDto>> GetCart([FromRoute] GetCartQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpPost]
        [Route("add")]
        public async Task<ActionResult> AddItem([FromBody] AddItemCommand cmd)
        {
            return Ok(await Mediator.Send(cmd));
        }

        [HttpDelete]
        [Route("remove")]
        public async Task<ActionResult> RemoveItem([FromBody] RemoveCartItemCommand cmd)
        {
            return Ok(await Mediator.Send(cmd));
        }

    }
}
