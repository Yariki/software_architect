using Catalog.Api.Policy;

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
            {
                options.AddPolicy(Policies.Read, policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim("permissions", "catalog.read");
                });
                options.AddPolicy(Policies.FullAccess, policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim("permissions", "catalog.read");
                    policy.RequireClaim("permissions", "catalog.create");
                    policy.RequireClaim("permissions", "catalog.update");
                    policy.RequireClaim("permissions", "catalog.delete");
                });
            }
            
        ); 

        return services;
    }
    
}