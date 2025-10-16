using AutoMapper;
using TiendaAPI.Application.DTOs;
using TiendaAPI.Domain.Interfaces;
using TiendaAPI.Infrastructure.Data.Entities;

namespace TiendaAPI.Application.Services;

public class CategoriaService : ICategoriaService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CategoriaService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CategoriaDto>> GetAllAsync()
    {
        // El nombre en tu repo es GetAll, no GetAllAsync
        var categorias = await _unitOfWork.Repository<Categoria>().GetAll(); 
        return _mapper.Map<IEnumerable<CategoriaDto>>(categorias);
    }

    public async Task<CategoriaDto?> GetByIdAsync(int id)
    {
        // El nombre en tu repo es GetById, no GetByIdAsync
        var categoria = await _unitOfWork.Repository<Categoria>().GetById(id); 
        return _mapper.Map<CategoriaDto>(categoria);
    }

    public async Task<CategoriaDto> CreateAsync(CategoriaCreateDto categoriaCreateDto)
    {
        var categoria = _mapper.Map<Categoria>(categoriaCreateDto);

        // Usamos Add, que solo agrega al contexto, NO guarda.
        await _unitOfWork.Repository<Categoria>().Add(categoria); 
        
        // El Unit of Work es quien guarda todos los cambios.
        await _unitOfWork.Complete();

        return _mapper.Map<CategoriaDto>(categoria);
    }

    public async Task UpdateAsync(int id, CategoriaUpdateDto categoriaUpdateDto)
    {
        var categoriaExistente = await _unitOfWork.Repository<Categoria>().GetById(id);
        if (categoriaExistente == null)
        {
            throw new KeyNotFoundException($"No se encontró la categoría con el ID {id}");
        }
        
        _mapper.Map(categoriaUpdateDto, categoriaExistente);

        // Usamos Update, que solo marca la entidad como modificada.
        await _unitOfWork.Repository<Categoria>().Update(categoriaExistente);

        // El Unit of Work guarda los cambios.
        await _unitOfWork.Complete();
    }

    public async Task DeleteAsync(int id)
    {
        // 1. Primero verificamos que la entidad exista
        var categoriaExistente = await _unitOfWork.Repository<Categoria>().GetById(id);
        if (categoriaExistente == null)
        {
            throw new KeyNotFoundException($"No se encontró la categoría con el ID {id}");
        }

        // 2. Usamos Delete, que en tu repo la busca de nuevo y la remueve del contexto.
        await _unitOfWork.Repository<Categoria>().Delete(id);
        
        // 3. El Unit of Work guarda los cambios.
        await _unitOfWork.Complete();
    }
}