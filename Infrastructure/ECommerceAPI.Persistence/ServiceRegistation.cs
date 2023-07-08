using ECommerceAPI.Application.Abstractions;
using ECommerceAPI.Persistence.Concrete;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerceAPI.Persistence;

public static class ServiceRegistation
{
    public static void AddPersistenceServices(this IServiceCollection services)
    {
        services.AddSingleton<IProductService,ProductService>();
    }
}