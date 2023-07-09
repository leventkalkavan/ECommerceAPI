using ECommerceAPI.Application.Abstractions;
using ECommerceAPI.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerceAPI.Persistence;

public static class ServiceRegistation
{
    public static void AddPersistenceServices(this IServiceCollection services)
    {
        services.AddDbContext<ECommcerAPIDbContext>(options => options.UseSqlServer(Configuration.ConnectionString));
    }
}