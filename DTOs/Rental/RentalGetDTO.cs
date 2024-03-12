using Sakila.App.WebAPI.Model;

namespace Sakila.App.WebAPI.DTOs;

public class RentalGetDTO
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public int InventoryId { get; set; }
    public int FilmId { get; set; }
    public string? CustomerName { get; set; }
    public string? FilmTitle { get; set; }
    public string? FilmReleaseYear { get; set; }
    public byte FilmRentalDuration { get; set; }
    public decimal FilmRentalRate { get; set; }
    public decimal PaymentAmount { get; set; }
    public DateTime RentalDate { get; set; }
    public DateTime? ReturnDate { get; set; }

    public static RentalGetDTO MapRentalToDTO(Rental r)
    {
        if (r.Customer == null || r.Inventory == null) 
        {
            return new RentalGetDTO()
            {
                Id = r.Id,
                CustomerId = r.CustomerId,
                InventoryId = r.InventoryId,
                RentalDate = r.RentalDate,
                ReturnDate = r.ReturnDate,
                PaymentAmount = r.Payment != null ? r.Payment.Amount : 0
            };
        }

        return new RentalGetDTO()
        {
            Id = r.Id,
            InventoryId = r.InventoryId,
            CustomerId = r.CustomerId,
            CustomerName = $"{r.Customer.FirstName} {r.Customer.LastName}",
            FilmId = r.Inventory.FilmId,
            FilmTitle = r.Inventory.Film.Title,
            FilmReleaseYear = r.Inventory.Film.ReleaseYear,
            FilmRentalDuration = r.Inventory.Film.RentalDuration,
            FilmRentalRate = r.Inventory.Film.RentalRate,
            PaymentAmount = r.Payment != null ? r.Payment.Amount : 0,
            RentalDate = r.RentalDate,
            ReturnDate = r.ReturnDate
        };
    }
}