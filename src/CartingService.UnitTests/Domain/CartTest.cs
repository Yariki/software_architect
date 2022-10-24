using CartingService.Application.Exceptions;
using CartingService.Domain.Entities;
using FluentAssertions;

namespace CartingService.UnitTests.Domain;

public class CartTest
{
    //todo is this possible for this test to fail?
    [Fact]
    public void CartCreate_Success()
    {
        var cart = new Cart();

        cart.Items.Should().NotBeNull();
    }

    [Fact]
    public void CartAddItem_Success() 
    {
        Guid cartId = Guid.NewGuid ();
        var cart = CreateCartFirst(cartId);

        cart.AddItem(new CartItem()
        {
            Id = 1,
            Name = "Test",
            Quantity = 1,
            Price = 1
        });


        cart.Items.Should().NotBeNull();
        cart.Items.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void CartAddItemUpdateExisting_Success()
    {
        var cartItem = new CartItem()
        {
            Id = 1,
            Name = "Test",
            Quantity = 1,
            Price = 1
        };
		Guid cartId = Guid.NewGuid();
		var cart = CreateCartSecond(cartId, cartItem);

        cart.AddItem(cartItem);

		cart.Items.Should().NotBeNull();
		cart.Items.Count.Should().BeGreaterThan(0);
       
        var temp = cart.Items.First(c => c.Id == cartItem.Id);
        temp.Should().NotBeNull();
        temp.Quantity.Should().Be(2);
	}

	[Fact]
	public void CartRemoveItem_Success()
	{
		var cartItem = new CartItem()
		{
			Id = 1,
			Name = "Test",
			Quantity = 1,
			Price = 1
		};
		Guid cartId = Guid.NewGuid();
		var cart = CreateCartSecond(cartId, cartItem);

        cart.RemoveItem(cartItem.Id);

        cart.Items.Count.Should().Be(0);
	}

	[Fact]
	public void CartRemoveItem_ThrowException()
	{
		
		Guid cartId = Guid.NewGuid();
		var cart = CreateCartFirst(cartId);

		Action act = () => cart.RemoveItem(1);

		act.Should().Throw<CartItemNotFoundException>();
	}



	private Cart CreateCartFirst(Guid cartId)
    {
        var cart = new Cart()
        {
            Id = cartId,
        };

        return cart; 
    }

	private Cart CreateCartSecond(Guid cartId, CartItem item)
	{
		var cart = new Cart()
		{
			Id = cartId,
		};
        cart.Items.Add(item);
		return cart;
	}
}