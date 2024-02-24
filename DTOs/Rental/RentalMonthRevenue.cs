namespace Sakila.App.WebAPI.DTOs;

public class RentalMonthRevenueDTO
{
    public string Month { get; set; } = null!;
    public string Year { get; set; } = null!;
    public decimal Revenue { get; set; }
}