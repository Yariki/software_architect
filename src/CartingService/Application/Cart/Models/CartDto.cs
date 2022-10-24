using CartingService.Application.Common.Mappings;
using CartingService.Domain.Entities;

namespace CartingService.Application.Cart.Models
{
    public class CartDto : IMapFrom<Domain.Entities.Cart>
    {

        public Guid Id { get; set; }

        public IList<CartItem> Items { get; private set; } = new List<CartItem>();
    }
}
