using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sakila.App.WebAPI.Model;

[Table("rental")]
public class Rental
{
    [Column("rental_id")]
    public int Id { get; set; }

    [Required]
    [Column("rental_date")]
    public DateTime RentalDate { get; set; }

    [Required]
    [Column("customer_id")]
    [ForeignKey("customer_id")]
    public int CustomerId { get; set; }

    public Customer Customer { get; set; } = null!;

    [Required]
    [Column("inventory_id")]
    [ForeignKey("inventory_id")]
    public int InventoryId { get; set; }

    public Inventory Inventory { get; set; } = null!;

    [Column("return_date")]
    public DateTime ReturnDate { get; set; }

    [Column("last_update")]
    public DateTime LastUpdate { get; set; }
}

