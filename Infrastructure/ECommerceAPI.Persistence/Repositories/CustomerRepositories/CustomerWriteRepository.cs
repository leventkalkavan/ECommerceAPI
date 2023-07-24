using ECommerceAPI.Application.Repositories.CustomerRepository;
using ECommerceAPI.Domain.Entities;
using ECommerceAPI.Persistence.Contexts;

namespace ECommerceAPI.Persistence.Repositories.CustomerRepository;

public class CustomerWriteRepository:WriteRepository<Customer> , ICustomerWriteRepository
{
    public CustomerWriteRepository(ECommerceAPIDbContext context) : base(context)
    {
    }
}