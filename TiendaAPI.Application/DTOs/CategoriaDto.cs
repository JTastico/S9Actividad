namespace TiendaAPI.Application.DTOs;

public class CategoriaDto
{
    public int Categoriaid { get; set; }
    public string Nombre { get; set; }
}
public class CategoriaCreateDto
{
    public string Nombre { get; set; }
}
public class CategoriaUpdateDto
{
    public string Nombre { get; set; }
}