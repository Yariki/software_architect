namespace Catalog.Api.Extensions;

public static class ServiceCollectionExtensions
{

    public static IServiceCollection AddIdentity(this IServiceCollection services)
    {
        services.AddAuthentication("Bearer")
            .AddJwtBearer(options =>
            {
                options.Authority = "https://localhost:5001";
                options.TokenValidationParameters.ValidateAudience = false;
            });
        services.AddAuthorization(options =>
            options.AddPolicy("CatalogScope", policy =>
            {
                policy.RequireAuthenticatedUser();
                policy.RequireClaim("scope", "catalog.full_access");
            })
        );

        return services;
    }
    
}