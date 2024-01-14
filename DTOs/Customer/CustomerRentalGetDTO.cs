using Sakila.App.WebAPI.Model;

namespace Sakila.App.WebAPI.DTOs;

public class CustomerRentalGetDTO
{
    public int Id { get; set; }
    public int FilmId { get; set; }
    public string? FilmTitle { get; set; }
    public string? FilmReleaseYear { get; set; }
    public byte FilmRentalDuration { get; set; }
    public decimal FilmRentalRate { get; set; }
    public DateTime RentalDate { get; set; }
    public DateTime ReturnDate { get; set; }

    public static CustomerRentalGetDTO MapRentalToDTO(Rental r)
    {
        if (r.Customer == null || r.Inventory == null) 
        {
            return new CustomerRentalGetDTO()
            {
                Id = r.Id,
                RentalDate = r.RentalDate,
                ReturnDate = r.ReturnDate
            };
        }

        return new CustomerRentalGetDTO()
        {
            Id = r.Id,
            FilmId = r.Inventory.FilmId,
            FilmTitle = r.Inventory.Film.Title,
            FilmReleaseYear = r.Inventory.Film.ReleaseYear,
            FilmRentalDuration = r.Inventory.Film.RentalDuration,
            FilmRentalRate = r.Inventory.Film.RentalRate,
            RentalDate = r.RentalDate,
            ReturnDate = r.ReturnDate
        };
    }
}