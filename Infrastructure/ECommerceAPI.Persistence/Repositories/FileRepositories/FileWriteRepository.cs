using ECommerceAPI.Application.Repositories.File;
using ECommerceAPI.Persistence.Contexts;
using File = ECommerceAPI.Domain.Entities.File;

namespace ECommerceAPI.Persistence.Repositories.FileRepositories;

public class FileWriteRepository : WriteRepository<File>, IFileWriteRepository
{
    public FileWriteRepository(ECommerceAPIDbContext context) : base(context)
    {
    }
}