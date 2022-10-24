using AutoMapper;
using CartingService.Application.Cart.Models;
using CartingService.Application.Exceptions;
using CartingService.Application.Interfaces;
using CartingService.Domain.Exceptions;

using MediatR;

namespace CartingService.Application.Cart.Commands.RemoveCartItem;

public class RemoveCartItemCommand : IRequest<CartDto>
{
    public Guid CartId { get; set; }

    public int ItemId { get; set; }
}

public class RemoveCartItemCommandHandler : IRequestHandler<RemoveCartItemCommand, CartDto>
{
    private ICartRepository _cartRepository;
    private IMapper _mapper;

    public RemoveCartItemCommandHandler(ICartRepository cartRepository, IMapper mapper)
    {
        _cartRepository = cartRepository;
        _mapper = mapper;
    }


    public Task<CartDto> Handle(RemoveCartItemCommand request, CancellationToken cancellationToken)
    {
        var cart = _cartRepository.GetCart(request.CartId);
        if (cart == null)
        {
            throw new CartServiceException("The cart does not exist");
        }

        cart.RemoveItem(request.ItemId);
        _cartRepository.UpdateCart(cart);

        return Task.FromResult(_mapper.Map<CartDto>(cart));
    }
}