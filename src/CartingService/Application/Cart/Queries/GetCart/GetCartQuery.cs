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
    private IUnitOfWork _unitOfWork;
    private IMapper _mapper;


    public GetCartQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<CartDto> Handle(GetCartQuery request, CancellationToken cancellationToken)
    {
        var cart = _unitOfWork.CartRepository.GetCart(request.Id);
        _unitOfWork.Dispose();
        return _mapper.Map<CartDto>(cart);
    }
}