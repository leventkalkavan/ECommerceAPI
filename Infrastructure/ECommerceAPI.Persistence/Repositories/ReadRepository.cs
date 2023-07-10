using System.Linq.Expressions;
using System.Runtime.Intrinsics.X86;
using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Domain.Entities.Common;
using ECommerceAPI.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace ECommerceAPI.Persistence.Repositories;

public class ReadRepository<T>:IReadRepository<T> where T:BaseEntity
{
    private readonly ECommerceAPIDbContext _context;
    public DbSet<T> Table => _context.Set<T>();
    
    public ReadRepository(ECommerceAPIDbContext context)
    {
        _context = context;
    }


    public IQueryable<T> GetAll() => Table;

    public IQueryable<T> GetWhere(Expression<Func<T, bool>> method) => Table.Where(method);

    public async Task<T> GetSingleAsync(Expression<Func<T, bool>> method) => await Table.FirstOrDefaultAsync();

    public async Task<T> GetByIdAsync(string id) => await Table.FirstOrDefaultAsync(x => x.Id == Guid.Parse(id));
}