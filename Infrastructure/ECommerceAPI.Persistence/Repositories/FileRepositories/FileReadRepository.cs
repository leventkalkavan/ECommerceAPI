using ECommerceAPI.Application.Repositories.File;
using ECommerceAPI.Persistence.Contexts;
using File = ECommerceAPI.Domain.Entities.File;

namespace ECommerceAPI.Persistence.Repositories.FileRepositories;

public class FileReadRepository : ReadRepository<File>, IFileReadRepository
{
    public FileReadRepository(ECommerceAPIDbContext context) : base(context)
    {
    }
}