using CartingService.Domain.Entities;
using FluentAssertions;

namespace CartingService.UnitTests.Domain;

public class CartTest
{
    [Fact]
    public void CartCreate_Success()
    {
        var cart = new Cart();

        cart.Items.Should().NotBeNull();
    }
}