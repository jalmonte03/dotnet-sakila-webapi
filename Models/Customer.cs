using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sakila.App.WebAPI.Model;

[Table("customer")]
public class Customer
{
    [Column("customer_id")]
    public int Id { get; set; }
    
    [Required]
    [MaxLength(45)]
    [Column("first_name")]
    public string? FirstName { get; set; }

    [Required]
    [MaxLength(45)]
    [Column("last_name")]
    public string? LastName { get; set; }

    [MaxLength(50)]
    public string? Email { get; set; }
    // public int Address_Id { get; set; } Address Table
    
    [Required]
    public char Active { get; set; }
    
    [Column("create_date")]
    public DateTime CreateDate { get; set; }
    
    [Column("last_update")]
    public DateTime LastUpdate { get; set; }

    [Column("address_id")]
    public int AddressId { get; set; }
    public virtual Address Address { get; set; } = null!;

    [NotMapped]
    public string? FullName => $"{FirstName} {LastName}";
}