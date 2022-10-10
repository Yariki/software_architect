using CartingService.Application.Cart.Commands.AddCartItem;
using CartingService.Application.Cart.Commands.RemoveCartItem;
using CartingService.Application.Cart.Queries.GetCart;
using CartingService.ComponentTests.Core;

using FluentAssertions;

using Newtonsoft.Json;

using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace CartingService.ComponentTests.Cart;


//todo did you heard anything about testing pyramid?
public class CartTests : IClassFixture<CustomWebApplicationFactory<Program>>
{

    private readonly CustomWebApplicationFactory<Program> _factory;
    private readonly HttpClient _client;

    public CartTests(CustomWebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _client = _factory.CreateClient();

    }

    [Fact]
    public async Task GetCart_ReturnCart()
    {
        var response = await _client.GetAsync($"/cart/{Consts.CartId}");

        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        var cart = JsonConvert.DeserializeObject<CartDto>(content);
        cart.Should().NotBeNull();
    }

    [Fact]
    public async Task AddItem_ItemAddedSuccess()
    {
        //arrange
        var addedItem = new AddItemCommand()
        {
            CartId = Guid.Parse(Consts.CartId)
        };
        addedItem.Item = new AddCartItemDto()
        {
            Id = Consts.CartItemIdAdd,
            Image = "Image",
            Name = "Name",
            Price = 10,
            Quantity = 2
        };

        //act 
        var response = await _client.PostAsJsonAsync("/cart/add", addedItem);

        //assert

        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        var cart = JsonConvert.DeserializeObject<bool>(content);

        cart.Should().BeTrue();
    }

    [Fact]
    public async Task RemoveItem_RemovedItemSuccess()
    {
        //arrange
        var removeCommand = new RemoveCartItemCommand()
        {
            CartId = Guid.Parse(Consts.CartId),
            ItemId = Consts.CartItemIdRemove
        };

        //act
        var request = new HttpRequestMessage(HttpMethod.Delete, "/cart/remove")
        {
            Content = new StringContent(JsonConvert.SerializeObject(removeCommand), Encoding.UTF8, "application/json")
        };
        var response = await _client.SendAsync(request);

        //assert

        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        var cart = JsonConvert.DeserializeObject<bool>(content);

        cart.Should().BeTrue();
    }

}
