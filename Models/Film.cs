using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sakila.App.WebAPI.Model;

[Table("film")]
public class Film 
{
    [Column("film_id")]
    public int Id { get; set; }

    [Required]
    [MaxLength(255)]
    public string? Title { get; set; }
    public string? Description { get; set; }

    [MaxLength(4)]
    [Column("release_year")]
    public string? ReleaseYear { get; set; }

    [Required]
    [DefaultValue(3)]
    [Column("rental_duration")]
    public byte RentalDuration { get; set; }

    [Required]
    [DefaultValue(4.99)]
    [Column("rental_rate")]
    public decimal RentalRate { get; set; }

    public short Length { get; set; }

    [DefaultValue('G')]
    [MaxLength(10)]
    public string? Rating { get; set; }

    [Column("last_update")]
    public DateTime LastUpdate { get; set; }

    public ICollection<Category> Categories { get; } = [];
}