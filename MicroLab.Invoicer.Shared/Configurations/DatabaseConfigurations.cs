using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicroLab.Invoicer.Shared.DataAccess;
using MicroLab.Invoicer.Shared.Literals;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace MicroLab.Invoicer.Shared.Configurations
{
    public static  class DatabaseConfigurations
    {
        public static IServiceCollection AddDatabaseConfiguration(this IServiceCollection services)
        {
            services.AddDbContext<InvoicerDbContext>(options =>
            {
                options.UseSqlServer(
                    Environment.GetEnvironmentVariable(InvoicerAppConfigLiterals.DATA_BASE_CONNECTION_STRING_KEY_NAME));
            });
            return services;
        }
    }
}
