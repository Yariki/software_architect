using AutoMapper;

using CartingService.Application.Interfaces;

using MediatR;

namespace CartingService.Application.Cart.Queries.GetCart;

public class GetCartQuery : IRequest<CartDto>
{
    public Guid Id { get; set; }
}

public class GetCartQueryHandler : IRequestHandler<GetCartQuery, CartDto>
{
    private ICartRepository _cartRepository;
    private IMapper _mapper;


    public GetCartQueryHandler(ICartRepository cartRepository, IMapper mapper)
    {
        _cartRepository = cartRepository;
        _mapper = mapper;
    }

    public async Task<CartDto> Handle(GetCartQuery request, CancellationToken cancellationToken)
    {
        var cart = _cartRepository.GetCart(request.Id);
        _cartRepository.Dispose();
        return _mapper.Map<CartDto>(cart);
    }
}