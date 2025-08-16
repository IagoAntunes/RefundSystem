using Microsoft.Extensions.DependencyInjection;
using RefundSystem.Application.Services;

namespace RefundSystem.Application
{
    public static class ApplicationRegistration
    {
        public static IServiceCollection AddApplicationServices(
            this IServiceCollection services
        )
        {
            services.AddScoped<ICategoryService, CategoryService>();
            return services;
        }
    }
}
