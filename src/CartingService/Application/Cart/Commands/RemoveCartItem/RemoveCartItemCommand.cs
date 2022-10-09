using AutoMapper;

using CartingService.Application.Exceptions;
using CartingService.Application.Interfaces;
using CartingService.Domain.Exceptions;

using MediatR;

namespace CartingService.Application.Cart.Commands.RemoveCartItem;

public class RemoveCartItemCommand : IRequest<bool>
{
    public Guid CartId { get; set; }

    public int ItemId { get; set; }
}

public class RemoveCartItemCommandHandler : IRequestHandler<RemoveCartItemCommand, bool>
{
    private ICartRepository _cartRepository;
    private IMapper _mapper;

    public RemoveCartItemCommandHandler(ICartRepository cartRepository, IMapper mapper)
    {
        _cartRepository = cartRepository;
        _mapper = mapper;
    }


    public Task<bool> Handle(RemoveCartItemCommand request, CancellationToken cancellationToken)
    {
        var cart = _cartRepository.GetCart(request.CartId);
        if (cart == null)
        {
            throw new CartServiceException("The cart does not exist");
        }

        var item = cart.Items.FirstOrDefault(x => x.Id == request.ItemId);
        if (item == null)
        {
            throw new CartItemNotFoundException("The cart item does not exist");
        }

        cart.Items.Remove(item);
        _cartRepository.UpdateCart(cart);

        return Task.FromResult(true);
    }
}