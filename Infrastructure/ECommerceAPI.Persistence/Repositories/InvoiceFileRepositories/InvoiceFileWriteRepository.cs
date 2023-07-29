using ECommerceAPI.Application.Repositories.InvoiceFile;
using ECommerceAPI.Domain.Entities;
using ECommerceAPI.Persistence.Contexts;

namespace ECommerceAPI.Persistence.Repositories.InvoiceFileRepositories;

public class InvoiceFileWriteRepository : WriteRepository<InvoiceFile>, IInvoiceFileWriteRepository
{
    public InvoiceFileWriteRepository(ECommerceAPIDbContext context) : base(context)
    {
    }
}