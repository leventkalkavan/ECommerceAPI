using ECommerceAPI.Domain.Entities.Common;

namespace ECommerceAPI.Domain.Entities;

public class Product : BaseEntity
{
    public ICollection<Order> Orders { get; set; }
    public string Name { get; set; }
    public string Stock { get; set; }
    public float Price { get; set; }
    public ICollection<ProductImageFile> ProductImageFiles { get; set; }
}