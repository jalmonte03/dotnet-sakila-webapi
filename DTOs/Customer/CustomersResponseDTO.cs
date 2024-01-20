namespace Sakila.App.WebAPI.DTOs;

public class CustomersResponseDTO
{
    public int CurrentPage { get; set; }
    public int Total { get; set; }
    public IEnumerable<CustomerDTO> Customers { get;  set; } = [];
}