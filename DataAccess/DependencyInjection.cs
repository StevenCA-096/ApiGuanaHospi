using DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDataAccess(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<GuanaHospiContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("GuanaHospiDb") ?? throw new InvalidOperationException("Connection string 'GuanaHospiDb' not found.")));

            services.AddScoped<GuanaHospiContext>();

            services.AddDbContext<DataWarehouseContext>(DWoptions =>
            DWoptions.UseSqlServer(configuration.GetConnectionString("DataWarehouse") ?? throw new InvalidOperationException("Connection string 'DataWarehouse' not found.")));

            services.AddScoped<DataWarehouseContext>();

            return services;
        }
    }
}
