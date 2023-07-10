using ECommerceAPI.Application.Repositories.OrderRepository;
using ECommerceAPI.Domain.Entities;
using ECommerceAPI.Persistence.Contexts;

namespace ECommerceAPI.Persistence.Repositories.OrderRepository;

public class OrderWriteRepository:WriteRepository<Order>,IOrderWriteRepository
{
    public OrderWriteRepository(ECommerceAPIDbContext context) : base(context)
    {
    }
}