using System.Runtime.Serialization;
using AutoFixture.Xunit2;
using AutoMapper;
using CartingService.Application.Cart.Queries.GetCart;
using CartingService.Application.Common.Mappings;
using CartingService.Domain.Entities;
using FluentAssertions;

namespace CartingService.UnitTests.Application.Mapping;

public class MappingTests
{
    
    private readonly IConfigurationProvider _configuration;
    private readonly IMapper _mapper;

    public MappingTests()
    {
        _configuration = new MapperConfiguration(config => 
            config.AddProfile<MappingProfile>());

        _mapper = _configuration.CreateMapper();
    }

    [Fact]
    public void ShouldHaveValidConfiguration()
    {
        _configuration.AssertConfigurationIsValid();
    }

    [Theory, AutoData]
    public void ShouldSupportMappingFromCartToCartDto(Cart cart)
    {
        var cartDto = _mapper.Map<CartDto>(cart);
        cartDto.Should().NotBeNull();
    }

}