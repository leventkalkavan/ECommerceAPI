using ECommerceAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using File = ECommerceAPI.Domain.Entities.File;

namespace ECommerceAPI.Persistence.Contexts;

public class ECommerceAPIDbContext:DbContext
{
    public ECommerceAPIDbContext(DbContextOptions options):base(options)
    {}

    private DbSet<Order> Orders { get; set; }
    private DbSet<Product> Products { get; set; }        
    public DbSet<File> Files { get; set; }
    public DbSet<ProductImageFile> ProductImageFiles { get; set; }
    public DbSet<InvoiceFile> InvoiceFiles { get; set; }
}