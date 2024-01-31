namespace Sakila.App.WebAPI.DTOs;

public class RentalsGetDTO
{
    public int Page { get; set; }

    public int Total { get; set; }

    public IEnumerable<RentalGetDTO> rentals { get; set; } = null!;
}