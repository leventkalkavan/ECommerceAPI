using ECommerceAPI.Application.Repositories.CustomerRepository;
using ECommerceAPI.Domain.Entities;
using ECommerceAPI.Persistence.Contexts;

namespace ECommerceAPI.Persistence.Repositories.CustomerRepository;

public class CustomerReadRepository:ReadRepository<Customer>, ICustomerReadRepository
{
    public CustomerReadRepository(ECommerceAPIDbContext context) : base(context)
    {
    }
}