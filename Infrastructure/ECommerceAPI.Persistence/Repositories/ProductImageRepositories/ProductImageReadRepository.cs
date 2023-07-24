using ECommerceAPI.Application.ProductImageFile;
using ECommerceAPI.Domain.Entities;
using ECommerceAPI.Persistence.Contexts;

namespace ECommerceAPI.Persistence.Repositories.ProductImagesRepositories;

public class ProductImageReadRepository:ReadRepository<ProductImageFile>,IProductImageFileReadRepository
{
    public ProductImageReadRepository(ECommerceAPIDbContext context) : base(context)
    {
    }
}