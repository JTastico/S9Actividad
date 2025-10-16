using System.Collections;
using TiendaAPI.Domain.Interfaces;

namespace TiendaAPI.Infrastructure.Data.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly TiendaDbContext _context;
    private Hashtable? _repositories;


    
    public UnitOfWork(TiendaDbContext context)
    {
        _context = context;
        _repositories = new Hashtable();
    }

    // Método genérico existente (NO SE TOCA)
    public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class
    {
        var type = typeof(TEntity).Name;

        if (_repositories!.ContainsKey(type))
        {
            return (IGenericRepository<TEntity>)_repositories[type]!;
        }

        var repositoryType = typeof(GenericRepository<>);
        var repositoryInstance = Activator.CreateInstance(
            repositoryType.MakeGenericType(typeof(TEntity)), _context);

        if (repositoryInstance != null)
        {
            _repositories.Add(type, repositoryInstance);
            return (IGenericRepository<TEntity>)repositoryInstance;
        }

        throw new Exception($"No se pudo crear el repositorio para el tipo {type}");
    }

    
    // Guardar cambios
    public async Task<int> Complete()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }

}