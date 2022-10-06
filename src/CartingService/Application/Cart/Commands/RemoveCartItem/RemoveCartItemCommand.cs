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
    private IUnitOfWork _unitOfWork;
    private IMapper _mapper;

    public RemoveCartItemCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }


    public Task<bool> Handle(RemoveCartItemCommand request, CancellationToken cancellationToken)
    {
        var cart = _unitOfWork.CartRepository.GetCart(request.CartId);
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
        _unitOfWork.CartRepository.UpdateCart(cart);
        _unitOfWork.Dispose();

        return Task.FromResult(true);
    }
}