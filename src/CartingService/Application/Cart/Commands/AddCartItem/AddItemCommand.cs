using AutoMapper;

using CartingService.Application.Interfaces;
using CartingService.Domain.Entities;
using CartingService.Domain.Exceptions;

using MediatR;

namespace CartingService.Application.Cart.Commands.AddCartItem;

public class AddItemCommand : IRequest<Models.CartDto>
{
    public Guid CartId { get; set; }

    public AddCartItemDto Item { get; set; }
}


public class AddItemCommandHandler : IRequestHandler<AddItemCommand, CartDto>
{

    private ICartRepository _cartRepository;
    private IMapper _mapper;

    public AddItemCommandHandler(ICartRepository cartRepository, IMapper mapper)
    {
        _cartRepository = cartRepository;
        _mapper = mapper;
    }

    public Task<CartDto> Handle(AddItemCommand request, CancellationToken cancellationToken)
    {
        var cart = _cartRepository.GetCart(request.CartId);
        if (cart == null)
        {
            throw new CartServiceException("The cart does not exist");
        }
        
        var newItem = _mapper.Map<CartItem>(request.Item);

        cart.AddItem(newItem);
        _cartRepository.UpdateCart(cart);

        return Task.FromResult(_mapper.Map<CartDto>(cart));
    }
}
