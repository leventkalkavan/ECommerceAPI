using ECommerceAPI.Application.Abstractions;
using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Application.Repositories.CustomerRepository;
using ECommerceAPI.Application.Repositories.OrderRepository;
using ECommerceAPI.Application.Repositories.ProductRepository;
using ECommerceAPI.Persistence.Contexts;
using ECommerceAPI.Persistence.Repositories.CustomerRepository;
using ECommerceAPI.Persistence.Repositories.OrderRepository;
using ECommerceAPI.Persistence.Repositories.ProductRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerceAPI.Persistence;

public static class ServiceRegistation
{
    public static void AddPersistenceServices(this IServiceCollection services,
        ConfigurationManager configurationManager)
    {
        services.AddDbContext<ECommerceAPIDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString), ServiceLifetime.Singleton);
        services.AddSingleton<ICustomerReadRepository,CustomerReadRepository>();
        services.AddSingleton<ICustomerWriteRepository,CustomerWriteRepository>();
        services.AddSingleton<IOrderReadRepository,OrderReadRepository>();
        services.AddSingleton<IOrderWriteRepository,OrderWriteRepository>();
        services.AddSingleton<IProductReadRepository,ProductReadRepository>();
        services.AddSingleton<IProductWriteRepository,ProductWriteRepository>();
    }
}