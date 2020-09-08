using Fruits.Business.Interfaces;
using Fruits.Business.Notificacoes;
using Fruits.Business.Services;
using Fruits.Data.Context;
using Fruits.Data.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace Fruits.App.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<FruitsDBContext>();
            services.AddScoped<IFruitService, FruitService>();
            services.AddScoped<INotificator, Notificator>();
            services.AddScoped<IFruitRepository, FruitRepository>();
            return services;
        }
    }
}