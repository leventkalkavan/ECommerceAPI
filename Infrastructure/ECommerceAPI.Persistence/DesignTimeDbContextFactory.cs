using ECommerceAPI.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ECommerceAPI.Persistence;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ECommerceAPIDbContext>
{
    public ECommerceAPIDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ECommerceAPIDbContext>();
        optionsBuilder.UseSqlServer(Configuration.GetConnectionString);

        return new ECommerceAPIDbContext(optionsBuilder.Options);
    }
}