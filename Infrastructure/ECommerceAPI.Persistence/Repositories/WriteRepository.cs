using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Domain.Entities.Common;
using ECommerceAPI.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ECommerceAPI.Persistence.Repositories;

public class WriteRepository<T> : IWriteRepository<T> where T: BaseEntity
{
    private readonly ECommerceAPIDbContext _context;

    public WriteRepository(ECommerceAPIDbContext context)
    {
        _context = context;
    }

    public DbSet<T> Table => _context.Set<T>();
    public async Task<bool> AddAsync(T model)
    {
        EntityEntry<T> entityEntry = await Table.AddAsync(model);
        return entityEntry.State == EntityState.Added;
    }

    public async Task<bool> AddRangeAsync(List<T> model)
    {
        await Table.AddRangeAsync(model);
        return true;
    }

    public bool Remove(T model)
    {
        EntityEntry<T> entityEntry =  Table.Remove(model);
        return entityEntry.State == EntityState.Deleted; 
    }

    public async Task<bool> RemoveAsync(string id)
    {
        T model = Table.FirstOrDefault(x => x.Id == Guid.Parse(id));
        return Remove(model);
    }

    public bool RemoveRange(List<T> data)
    {
       Table.RemoveRange(data);
       return true;
    }

    public bool Update(T model)
    {
        EntityEntry<T> entityEntry = Table.Update(model);
        return entityEntry.State == EntityState.Modified;
    }

    public async Task<int> SaveAsync() => await _context.SaveChangesAsync();
}