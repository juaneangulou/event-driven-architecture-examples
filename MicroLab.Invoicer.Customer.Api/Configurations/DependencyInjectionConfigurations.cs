using MicroLab.Invoicer.Shared.Contracts;
using MicroLab.Invoicer.Shared.DataAccess.Models;
using MicroLab.Invoicer.Shared.DataAccess.Repositories;

namespace MicroLab.Invoicer.Customer.Api.Configurations
{
    public static class DependencyInjectionConfigurations
    {
        public static IServiceCollection AddDependencyInjectionConfigurations(this IServiceCollection services)
        {
            services.AddScoped<IBaseRepository<CustomerModel>, BaseRepository<CustomerModel>>();
            return services;
        }
    }
}
