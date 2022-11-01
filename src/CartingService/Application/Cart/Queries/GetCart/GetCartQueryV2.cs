using System.Collections;
using AutoMapper;
using CartingService.Application.Cart.Models;
using CartingService.Application.Interfaces;
using MediatR;

namespace CartingService.Application.Cart.Queries.GetCart;

public class GetCartQueryV2 : IRequest<IEnumerable<CartItemDto>>
{
    public Guid Id { get; set; }
}

public class GetCartQueryHandlerV2 : IRequestHandler<GetCartQueryV2, IEnumerable<CartItemDto>>
{
    private ICartRepository _cartRepository;
    private IMapper _mapper;


    public GetCartQueryHandlerV2(ICartRepository cartRepository, IMapper mapper)
    {
        _cartRepository = cartRepository;
        _mapper = mapper;
    }
    
    public Task<IEnumerable<CartItemDto>> Handle(GetCartQueryV2 request, CancellationToken cancellationToken)
    {
        var cart = _cartRepository.GetCart(request.Id);
        return Task.FromResult<IEnumerable<CartItemDto>>(cart.Items.Select(s => _mapper.Map<CartItemDto>(s)).ToList());
    }
} 