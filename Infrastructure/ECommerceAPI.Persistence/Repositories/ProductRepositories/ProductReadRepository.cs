using ECommerceAPI.Application.Repositories.ProductRepository;
using ECommerceAPI.Domain.Entities;
using ECommerceAPI.Persistence.Contexts;

namespace ECommerceAPI.Persistence.Repositories.ProductRepository;

public class ProductReadRepository : ReadRepository<Product>, IProductReadRepository
{
    public ProductReadRepository(ECommerceAPIDbContext context) : base(context)
    {
    }
}