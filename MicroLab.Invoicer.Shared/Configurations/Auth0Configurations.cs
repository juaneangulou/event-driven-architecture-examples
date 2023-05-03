using MicroLab.Invoicer.Shared.Literals;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;

namespace MicroLab.Invoicer.Shared.Configurations
{
    public static class Auth0Configurations
    {
        public static IServiceCollection AddAuth0Configuration(this IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.Authority = Environment.GetEnvironmentVariable(InvoicerAppConfigLiterals.AUTHORITY_KEY_NAME);
                options.Audience = Environment.GetEnvironmentVariable(InvoicerAppConfigLiterals.AUDIENCE_KEY_NAME);
            });
            return services;
        }
    }
}
