using CartingService.Application.Common.Mappings;

namespace CartingService.Application.Cart.Queries.GetCart;

public class CartDto : IMapFrom<Domain.Entities.Cart>
{
      
    public Guid Id { get; set; }

    public IList<CartItemDto> Items { get; private set; } = new List<CartItemDto>();
}