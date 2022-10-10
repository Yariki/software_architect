using CartingService.Application.Interfaces;
using CartingService.Domain.Entities;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

using System;

namespace CartingService.ComponentTests.Core
{
    public class CustomWebApplicationFactory<TStartup>
    : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var sp = services.BuildServiceProvider();

                using (var scope = sp.CreateScope())
                {
                    SeedDb(scope);

                }
            });
        }

        private static void SeedDb(IServiceScope scope)
        {
            var scopedServices = scope.ServiceProvider;
            var db = scopedServices.GetRequiredService<IApplicationDbContext>();

            try
            {
                var collection = db.Database.GetCollection<Domain.Entities.Cart>();
                var cart = new Domain.Entities.Cart()
                {
                    Id = Guid.Parse(Consts.CartId)
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
    }
}
