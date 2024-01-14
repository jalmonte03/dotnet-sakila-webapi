namespace Sakila.App.WebAPI.DTOs;

public class CustomerRentalsDTO
{
    public IEnumerable<CustomerRentalGetDTO> Rentals { get; set; } = null!;
    public int Page { get; set; }
    public int Total { get; set; }
}