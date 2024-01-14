namespace Sakila.App.WebAPI.DTOs;

public class CustomerWatchedCategoryDTO
{
    public string? Category { get; set; }
    public ushort MoviesWatched { get; set; } 
}