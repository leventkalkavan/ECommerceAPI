using ECommerceAPI.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace ECommerceAPI.Persistence;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ECommerceAPIDbContext>
{
    public ECommerceAPIDbContext CreateDbContext(string[] args)
    {
        DbContextOptionsBuilder<ECommerceAPIDbContext> dbContextOptionsBuilder = new();
        dbContextOptionsBuilder.UseSqlServer(Configuration.GetConnectionString);
        return new(dbContextOptionsBuilder.Options);
    }
}