namespace Sakila.App.WebAPI.DTOs;

public class CustomerSummaryDTO
{
    public int Id { get; set; }
    public string? CustomerName { get; set; }
    public int MoviesRentedTotal { get; set; }
    public int MoviesNotReturned { get; set; }
    public decimal TotalSpent { get; set; }
    public string? MostViewedCategory { get; set; }
}