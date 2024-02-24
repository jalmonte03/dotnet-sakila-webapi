namespace Sakila.App.WebAPI.DTOs;

public class RentalMonthSummaryDTO 
{
    public string Month { get; set; } = null!;
    public string Year { get; set; } = null!;
    public int Amount { get; set; }
}