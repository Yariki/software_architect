using CartingService.Application.Common.Mappings;
using CartingService.Domain.Entities;

namespace CartingService.Application.Cart.Queries.GetCart;

public class CartItemDto : IMapFrom<CartItem>
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Image { get; set; }

    public decimal Price { get; set; }

    public int Quantity { get; set; }
}