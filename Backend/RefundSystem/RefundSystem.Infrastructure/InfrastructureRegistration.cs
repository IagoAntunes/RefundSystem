using Microsoft.AspNetCore.Identity;
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
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IAuthRepository, AuthRepository>();


            services.AddDbContext<RefundSystemDbContext>(
               options => options.UseSqlServer(
                   configuration.GetConnectionString("RefundSystemConnectionString")
               )
            );

            services.AddDbContext<RefundSystemAuthDbContext>(
               options => options.UseSqlServer(
                   configuration.GetConnectionString("RefundSystemAuthConnectionString")
               )
            );

            services.AddIdentityCore<IdentityUser>()
                .AddRoles<IdentityRole>()
                .AddTokenProvider<DataProtectorTokenProvider<IdentityUser>>("RefundSystem")
                .AddEntityFrameworkStores<RefundSystemAuthDbContext>()
                .AddDefaultTokenProviders();

            return services;
        }
    }
}
