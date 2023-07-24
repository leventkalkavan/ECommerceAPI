using ECommerceAPI.Application.Repositories.OrderRepository;
using ECommerceAPI.Domain.Entities;
using ECommerceAPI.Persistence.Contexts;

namespace ECommerceAPI.Persistence.Repositories.OrderRepository;

public class OrderReadRepository: ReadRepository<Order>,IOrderReadRepository
{
    public OrderReadRepository(ECommerceAPIDbContext context) : base(context)
    {
    }
}