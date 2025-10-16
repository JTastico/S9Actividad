using AutoMapper;
using TiendaAPI.Application.DTOs;
using TiendaAPI.Infrastructure.Data.Entities;

namespace TiendaAPI.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Mapeo de la entidad Cliente al Cl        // 1. Mapeo para obtener datos: Entidad -> DTO
        CreateMap<Categoria, CategoriaDto>();

        // 2. Mapeo para crear: DTO -> Entidad
        CreateMap<CategoriaCreateDto, Categoria>();

        // 3. Mapeo para actualizar: DTO -> Entidad
        CreateMap<CategoriaUpdateDto, Categoria>();
    }
}