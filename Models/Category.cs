using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sakila.App.WebAPI.Model;

[Table("category")]
public class Category
{
    [Column("category_id")]
    public byte Id { get; set; }

    [Required]
    [MaxLength(25)]
    public string? Name { get; set; }

    [Column("last_update")]
    public DateTime LastUpdate { get; set; }

    public ICollection<Film> Films { get; } = [];
}