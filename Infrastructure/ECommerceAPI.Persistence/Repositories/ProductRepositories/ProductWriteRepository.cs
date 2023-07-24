using ECommerceAPI.Application.Repositories.ProductRepository;
using ECommerceAPI.Domain.Entities;
using ECommerceAPI.Persistence.Contexts;

namespace ECommerceAPI.Persistence.Repositories.ProductRepository;

public class ProductWriteRepository:WriteRepository<Product>, IProductWriteRepository
{
    public ProductWriteRepository(ECommerceAPIDbContext context) : base(context)
    {
    }
}