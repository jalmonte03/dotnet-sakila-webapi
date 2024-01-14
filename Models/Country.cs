using System.ComponentModel.DataAnnotations.Schema;

namespace Sakila.App.WebAPI.Model;

[Table("country")]
public class Country
{
    [Column("country_id")]
    public short Id { get; set; }

    [Column("country")]
    public string? CountryName { get; set; }

    [Column("last_update")]
    public DateTime LastUpdate { get; set; }

    public virtual ICollection<City> Cities { get; set; } = null!;
}