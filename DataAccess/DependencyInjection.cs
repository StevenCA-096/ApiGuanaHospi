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

            return services;
        }
    }
}
