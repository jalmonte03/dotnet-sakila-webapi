using Microsoft.EntityFrameworkCore;

namespace Sakila.App.WebAPI.Model;

[Keyless]
public class MonthlyRentalRevenueUDF
{
    public int Month { get; set; }
    public int Year { get; set; }
    public decimal Revenue { get; set; }
}