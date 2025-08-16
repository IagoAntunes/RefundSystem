using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RefundSystem.Domain.Repositories;
using RefundSystem.Infrastructure.Data;
using RefundSystem.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RefundSystem.Infrastructure
{
    public static class InfrastructureRegistration
    {
        public static IServiceCollection AddInfrastructureServices(
            this IServiceCollection services,
            IConfiguration configuration
        )
        {

            services.AddDbContext<RefundSystemDbContext>(
               options => options.UseSqlServer(
                   configuration.GetConnectionString("RefundSystemConnectionString"),
                   b => b.MigrationsAssembly("RefundSystem.Infrastructure")
               )
            );

            services.AddScoped<ICategoryRepository, CategoryRepository>();
            return services;
        }
    }
}
