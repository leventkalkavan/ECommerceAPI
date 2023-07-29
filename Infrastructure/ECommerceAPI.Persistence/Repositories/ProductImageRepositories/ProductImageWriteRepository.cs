using ECommerceAPI.Application.Repositories.ProductImageFile;
using ECommerceAPI.Domain.Entities;
using ECommerceAPI.Persistence.Contexts;

namespace ECommerceAPI.Persistence.Repositories.ProductImageRepositories;

public class ProductImageWriteRepository : WriteRepository<ProductImageFile>, IProductImageFileWriteRepository
{
    public ProductImageWriteRepository(ECommerceAPIDbContext context) : base(context)
    {
    }
}