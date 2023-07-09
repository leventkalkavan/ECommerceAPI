using ECommerceAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ECommerceAPI.Persistence.Contexts;

public class ECommerceAPIDbContext:DbContext
{
    public ECommerceAPIDbContext(DbContextOptions options):base(options)
    {}

    private DbSet<Order> Orders { get; set; }
    private DbSet<Product> Products { get; set; }
}