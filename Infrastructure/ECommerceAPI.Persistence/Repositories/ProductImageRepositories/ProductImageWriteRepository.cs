using ECommerceAPI.Application.ProductImageFile;
using ECommerceAPI.Domain.Entities;
using ECommerceAPI.Persistence.Contexts;

namespace ECommerceAPI.Persistence.Repositories.ProductImagesRepositories;

public class ProductImageWriteRepository:WriteRepository<ProductImageFile>,IProductImageFileWriteRepository
{
    public ProductImageWriteRepository(ECommerceAPIDbContext context) : base(context)
    {
    }
}