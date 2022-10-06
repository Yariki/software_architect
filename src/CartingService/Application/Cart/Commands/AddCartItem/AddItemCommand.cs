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


public class AddItemCommandHandler : IRequestHandler<AddItemCommand, bool>
{

    private IUnitOfWork _unitOfWork;
    private IMapper _mapper;

    public AddItemCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public Task<bool> Handle(AddItemCommand request, CancellationToken cancellationToken)
    {
        var cart = _unitOfWork.CartRepository.GetCart(request.CartId);
        if (cart == null)
        {
            throw new CartServiceException("The cart does not exist");
        }

        cart.Items.Add(_mapper.Map<CartItem>(request.Item));
        _unitOfWork.CartRepository.UpdateCart(cart);
        _unitOfWork.Dispose();

        return Task.FromResult(true);
    }
}
