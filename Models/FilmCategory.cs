using System.ComponentModel.DataAnnotations.Schema;

namespace Sakila.App.WebAPI.Model;

[Table("film_category")]
public class FilmCategory
{
    [Column("film_id")]
    public int FilmId { get; set; }

    [Column("category_id")]
    public byte CategoryId { get; set; }

    public Film Film { get; set; } = null!;

    public Category Category { get; set; } = null!;

    [Column("last_update")]
    public DateTime LastUpdate { get; set; }
}