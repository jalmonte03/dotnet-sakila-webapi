namespace Sakila.App.WebAPI.DTOs;

public class FilmsGetDTO
{
    public int Page { get; set; }

    public int Total { get; set; }

    public IEnumerable<FilmGetDTO> films { get; set; } = null!;
}