using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sakila.App.WebAPI.Model;

[Table("payment")]
public class Payment
{
    [Column("payment_id")]
    public int Id { get; set; } 

    [Required]
    [Column("customer_id")]
    public int CustomerId { get; set; }

    [Column("rental_id")]
    public int? RentalId { get; set; }

    [Required]
    public decimal Amount { get; set; }
    
    [Column("payment_date")]
    public DateTime PaymentDate { get; set; }

    [Column("last_update")]
    public DateTime LastUpdate { get; set; }

    public Rental Rental { get; set; } = null!;
    public Customer Customer { get; set; } = null!;
}