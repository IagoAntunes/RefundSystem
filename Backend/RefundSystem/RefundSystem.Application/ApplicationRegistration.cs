using Microsoft.Extensions.DependencyInjection;
using RefundSystem.Application.Services.Implementations;
using RefundSystem.Application.Services.Interfaces;

namespace RefundSystem.Application
{
    public static class ApplicationRegistration
    {
        public static IServiceCollection AddApplicationServices(
            this IServiceCollection services
        )
        {
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IRefundService, RefundService>();
            return services;
        }
    }
}
