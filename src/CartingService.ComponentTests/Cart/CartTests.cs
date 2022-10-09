using System;
using System.Net.Http;
using System.Threading.Tasks;
using CartingService.Application.Cart.Queries.GetCart;
using CartingService.Application.Interfaces;
using CartingService.Domain.Entities;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace CartingService.ComponentTests.Cart;



public class CartTests : IClassFixture<CustomWebApplicationFactory<Program>>
{

    internal const string CartId = "DE916D22-9530-46A0-82A4-80E623B5648E";
    
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
         var response = await _client.GetAsync($"/cart/{CartId}");

         response.EnsureSuccessStatusCode();
         var content = await response.Content.ReadAsStringAsync();
         var cart = JsonConvert.DeserializeObject<CartDto>(content);
         cart.Should().NotBeNull();
    }
    
}

public class CustomWebApplicationFactory<TStartup>
    : WebApplicationFactory<TStartup> where TStartup: class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var sp = services.BuildServiceProvider();

            using (var scope = sp.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                var db = scopedServices.GetRequiredService<IApplicationDbContext>();

                try
                {
                    var collection = db.Database.GetCollection<Domain.Entities.Cart>();
                    var cart = new Domain.Entities.Cart()
                    {
                        Id = Guid.Parse(CartTests.CartId)
                    };
                    cart.Items.Add(new CartItem()
                        {
                            Id = 1,
                            Image = string.Empty,
                            Price = 10.5m,
                            Name = "Test product",
                            Quantity = 1
                        });
                    collection.Insert(cart);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    
                }
                
            }
        });
    }

}
