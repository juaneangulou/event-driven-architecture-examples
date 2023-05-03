using MicroLab.Invoicer.Shared.Contracts;
using MicroLab.Invoicer.Shared.DataAccess.Models;
using MicroLab.Invoicer.Shared.DataAccess.Repositories;

namespace MicroLab.Invoicer.Supplier.Api.Configurations
{
    public static class DependencyInjectionConfigurations
    {
        public static IServiceCollection AddDependencyInjectionConfigurations(this IServiceCollection services)
        {
            services.AddScoped<IBaseRepository<SupplierModel>, BaseRepository<SupplierModel>>();
            return services;
        }
    }
}
