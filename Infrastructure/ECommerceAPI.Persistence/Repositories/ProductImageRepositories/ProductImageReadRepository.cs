using ECommerceAPI.Application.Repositories.ProductImageFile;
using ECommerceAPI.Domain.Entities;
using ECommerceAPI.Persistence.Contexts;

namespace ECommerceAPI.Persistence.Repositories.ProductImageRepositories;

public class ProductImageReadRepository : ReadRepository<ProductImageFile>, IProductImageFileReadRepository
{
    public ProductImageReadRepository(ECommerceAPIDbContext context) : base(context)
    {
    }
}