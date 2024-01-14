using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sakila.App.WebAPI.Model;

[Table("address")]
public class Address
{
    [Column("address_id")]
    public int Id { get; set; }
    
    [Column("address")]
    [Required]
    [MaxLength(50)]
    public string? StreetAddress { get; set; }
    
    [Column("address2")]
    [MaxLength(50)]
    public string? StreetAddress2 {  get; set; }

    [Required]
    [MaxLength(20)]
    public string? District { get; set; }

    [Column("postal_code")]
    [MaxLength(10)]
    public string? PostalCode { get; set; }

    [Required]
    [MaxLength(20)]
    public string? Phone { get; set; }

    [Column("last_update")]
    public DateTime LastUpdate { get; set; }

    [Column("city_id")]
    public int CityId { get; set; }
    public virtual City City { get; set; } = null!;
}