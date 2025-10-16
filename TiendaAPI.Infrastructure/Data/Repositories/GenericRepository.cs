using Microsoft.EntityFrameworkCore;
using TiendaAPI.Domain.Interfaces;

namespace TiendaAPI.Infrastructure.Data.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly TiendaDbContext _context;

    public GenericRepository(TiendaDbContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<T>> GetAll() => await _context.Set<T>().ToListAsync();

    public async Task<T?> GetById(int id) => await _context.Set<T>().FindAsync(id);

    public async Task AddAsync(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
        await _context.SaveChangesAsync();
    }
    
    public async Task Add(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
    }

    public async Task Update(T entity)
    {
        _context.Set<T>().Update(entity);
    }

    public async Task Delete(int id)
    {
        var entity = await _context.Set<T>().FindAsync(id);
        if (entity != null)
        {
            _context.Set<T>().Remove(entity);
        }
    }
}