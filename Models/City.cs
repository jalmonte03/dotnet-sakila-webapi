using System.ComponentModel.DataAnnotations.Schema;

namespace Sakila.App.WebAPI.Model;

[Table("city")]
public class City
{
    [Column("city_id")]
    public int Id { get; set; }

    [Column("city")]
    public string? CityName { get; set; }

    [Column("last_update")]
    public DateTime LastUpdate { get; set; }
    
    [Column("country_id")]
    [ForeignKey("country_id")]
    public short CountryId { get; set; }

    public virtual Country Country { get; set; } = null!;
}