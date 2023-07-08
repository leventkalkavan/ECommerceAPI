using ECommerceAPI.Application.Abstractions;
using ECommerceAPI.Domain.Entities;

namespace ECommerceAPI.Persistence.Concrete;

public class ProductService:IProductService
{
    public List<Product> GetProduct()
        => new()
        {
            new()
            {
                Id = Guid.NewGuid(),
                CreatedDate = DateTime.Now,
                Name = "Urun1",
                Stock = "2",
                Price = 40
            },
            new()
            {
                Id = Guid.NewGuid(),
                CreatedDate = DateTime.Now,
                Name = "Urun3",
                Stock = "2",
                Price = 30
            },
            new()
            {
                Id = Guid.NewGuid(),
                CreatedDate = DateTime.Now,
                Name = "Urun3",
                Stock = "3",
                Price = 60
            }
        };
}