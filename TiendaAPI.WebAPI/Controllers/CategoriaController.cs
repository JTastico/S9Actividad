using Microsoft.AspNetCore.Mvc;
using TiendaAPI.Application.DTOs;
using TiendaAPI.Application.Services;

namespace TiendaAPI.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriasController : ControllerBase
{
    private readonly ICategoriaService _categoriaService;

    public CategoriasController(ICategoriaService categoriaService)
    {
        _categoriaService = categoriaService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var categorias = await _categoriaService.GetAllAsync();
        return Ok(categorias);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var categoria = await _categoriaService.GetByIdAsync(id);
        if (categoria == null)
        {
            return NotFound();
        }
        return Ok(categoria);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CategoriaCreateDto createDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var nuevaCategoria = await _categoriaService.CreateAsync(createDto);
        return CreatedAtAction(nameof(GetById), new { id = nuevaCategoria.Categoriaid }, nuevaCategoria);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, CategoriaUpdateDto updateDto)
    {
        try
        {
            await _categoriaService.UpdateAsync(id, updateDto);
            return NoContent(); // Éxito, sin contenido que devolver
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _categoriaService.DeleteAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }
}