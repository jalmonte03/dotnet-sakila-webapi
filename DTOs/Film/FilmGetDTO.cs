using Sakila.App.WebAPI.Model;

namespace Sakila.App.WebAPI.DTOs;

public class FilmGetDTO
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? ReleaseYear { get; set; }
    public byte RentalDuration { get; set; }
    public decimal RentalRate { get; set; }
    public decimal ReplacementCost { get; set; }
    public short Length { get; set; }
    public string? Rating { get; set; }
    public DateTime LastUpdate { get; set; }
    public IEnumerable<CategoryDTO> Categories { get; set; } = [];

    public static FilmGetDTO MapFilmToDTO(Film f)
    {
        return new FilmGetDTO()
        {
            Id = f.Id,
            Title = f.Title,
            Description = f.Description,
            ReleaseYear = f.ReleaseYear,
            RentalDuration = f.RentalDuration,
            RentalRate = f.RentalRate,
            ReplacementCost = f.ReplacementCost,
            Length = f.Length,
            Rating = f.Rating,
            LastUpdate = f.LastUpdate,
            Categories = f.Categories.Select(CategoryDTO.MapCategoryToDTO)
        };
    }
}