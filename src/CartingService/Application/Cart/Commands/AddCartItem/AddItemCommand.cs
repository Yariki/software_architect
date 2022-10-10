using AutoMapper;

using CartingService.Application.Interfaces;
using CartingService.Domain.Entities;
using CartingService.Domain.Exceptions;

using MediatR;

namespace CartingService.Application.Cart.Commands.AddCartItem;

public class AddItemCommand : IRequest<bool>
{
    public Guid CartId { get; set; }

    public AddCartItemDto Item { get; set; }
}


public class AddItemCommandHandler : IRequestHandler<AddItemCommand, bool> //todo is the response as bool looks ok to you?
{

    private ICartRepository _cartRepository;
    private IMapper _mapper;

    public AddItemCommandHandler(ICartRepository cartRepository, IMapper mapper)
    {
        _cartRepository = cartRepository;
        _mapper = mapper;
    }

    public Task<bool> Handle(AddItemCommand request, CancellationToken cancellationToken)
    {
        var cart = _cartRepository.GetCart(request.CartId);
        if (cart == null)
        {
            //todo how does exceptions are handled
            throw new CartServiceException("The cart does not exist");
        }
        
        var newItem = _mapper.Map<CartItem>(request.Item);
        
        //todo this looks like business logic, shouldn't it be in the Domain layer instead?
        var existingItem = cart.Items.FirstOrDefault(x => x.Id == newItem.Id);
        if (existingItem == null)
        {
            cart.Items.Add(newItem);    
        }
        else
        {
            existingItem.Quantity += newItem.Quantity;
            existingItem.Price = newItem.Price;
        }
        
        _cartRepository.UpdateCart(cart);

        return Task.FromResult(true);
    }
}
