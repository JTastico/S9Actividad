using TiendaAPI.Application.DTOs;

namespace TiendaAPI.Application.Services;

public interface ICategoriaService
{
    Task<IEnumerable<CategoriaDto>> GetAllAsync();
    Task<CategoriaDto> GetByIdAsync(int id);
    Task<CategoriaDto> CreateAsync(CategoriaCreateDto categoriaCreateDto);
    Task UpdateAsync(int id, CategoriaUpdateDto categoriaUpdateDto);
    Task DeleteAsync(int id);
}