using ECommerceAPI.Application.Abstractions;
using ECommerceAPI.Persistence.Contexts;
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
            options.UseSqlServer(Configuration.GetConnectionString));
    }
}