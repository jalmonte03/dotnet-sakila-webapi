using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sakila.App.WebAPI.Model;

[Table("inventory")]
public class Inventory
{
    [Column("inventory_id")]
    public int Id { get; set; }

    [Required]
    [Column("film_id")]
    [ForeignKey("film_id")]
    public int FilmId { get; set; }

    [Column("last_update")]
    public DateTime LastUpdate { get; set; }

    public Film Film { get; set; } = null!;
}