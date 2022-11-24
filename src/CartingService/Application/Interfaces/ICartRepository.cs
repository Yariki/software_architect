using System.Linq.Expressions;
using CartingService.Domain.Entities;

namespace CartingService.Application.Interfaces;

public interface ICartRepository
{
    Domain.Entities.Cart GetCart(Guid id);
    IEnumerable<Domain.Entities.Cart> GetCartWithProduct(int id);
    void UpdateCart(Domain.Entities.Cart cart);
}
