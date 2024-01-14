using Sakila.App.WebAPI.Model;

namespace Sakila.App.WebAPI.DTOs;

public class CategoryDTO
{
    public byte Id { get; set; }
    public string? Name { get; set; }

    public static CategoryDTO MapCategoryToDTO(Category c)
    {
        return new CategoryDTO()
        {
            Id = c.Id,
            Name = c.Name
        };
    }
}