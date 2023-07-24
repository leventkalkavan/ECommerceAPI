using ECommerceAPI.Application.Abstractions.Storage;
using ECommerceAPI.Infrastructure.Storage;
using ECommerceAPI.Infrastructure.Storage.Local;
using ECommerceAPI.Infrastructure.Enums;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerceAPI.Infrastructure;

public static class ServiceRegistation
{
    public static void AddInfrastructureServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IStorageService, StorageService>();
    }
    public static void AddStorage<T>(this IServiceCollection serviceCollection) where T : class, IStorage
    {
        serviceCollection.AddScoped<IStorage, T>();
    }
    public static void AddStorage(this IServiceCollection serviceCollection, StorageType storageType)
    {
        switch (storageType)
        {
            case StorageType.Local:
                serviceCollection.AddScoped<IStorage, LocalStorage>();
                break;
            case StorageType.Azure:

                break;
            case StorageType.AWS:

                break;
            default:
                serviceCollection.AddScoped<IStorage, LocalStorage>();
                break;
        }
    }
}
