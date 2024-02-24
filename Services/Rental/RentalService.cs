using Microsoft.EntityFrameworkCore;
using Sakila.App.WebAPI.Context;
using Sakila.App.WebAPI.DTOs;
using Sakila.App.WebAPI.Model;

namespace Sakila.App.WebAPI.Service;

public class RentalService : IRentalService
{
    SakilaContext db;

    public RentalService(SakilaContext db)
    {
        this.db = db;
    }

    public async Task<RentalGetDTO?> GetRental(int Id)
    {
        Rental? rental = await db.Rentals
            .Include(r => r.Customer)
            .Include(r => r.Inventory)
            .ThenInclude(i => i.Film)
            .FirstOrDefaultAsync(r => r.Id == Id);

        if (rental == null)
        {
            return null;
        } 

        return RentalGetDTO.MapRentalToDTO(rental);
    }

    public async Task<RentalsGetDTO> GetRentals(int Page = 1, int Limit = 10)
    {
        var rentalsQuery = db.Rentals.AsQueryable();
        var rentalsCount = rentalsQuery.Count();

        var rentals = await rentalsQuery
            .Include(r => r.Customer)
            .Include(r => r.Inventory)
            .ThenInclude(i => i.Film)
            .Skip((Page - 1) * Limit)
            .Take(Limit)
            .ToListAsync();

        var rentalsDto = rentals.Select(r => RentalGetDTO.MapRentalToDTO(r));

        return new RentalsGetDTO {
            Page = Page,
            Total = rentalsCount,
            rentals = rentalsDto
        };
    }

    public async Task<IEnumerable<RentalMonthSummaryDTO>> GetMonthlyRentalsSummary(DateOnly From, DateOnly To)
    {
        IQueryable<RentalMonthSummaryDTO> rentalSummaryQuery = from rental in db.Rentals
            where DateOnly.FromDateTime(rental.RentalDate) > From &&
                 DateOnly.FromDateTime(rental.RentalDate) < To
            group rental by new {
                rental.RentalDate.Month,
                rental.RentalDate.Year
            } into rsg
            select new RentalMonthSummaryDTO() {
                Month = rsg.Key.Month.ToString(), 
                Year = rsg.Key.Year.ToString(),
                Amount = rsg.Count()
            };

        var rentalMonthSummaries = await rentalSummaryQuery.ToListAsync();

        // Get the date range
        int initialMonth = From.Month;
        int initialYear = From.Year;
        int finalMonth = To.Month;
        int finalYear = To.Year;

        // The full list covering all months
        List<RentalMonthSummaryDTO> fullRentalMonthSummaries = new List<RentalMonthSummaryDTO>();

        // Rebuild the full rental summary including empty months
        for(int year = initialYear; year <= finalYear; year++)
        {
            for(int month = initialMonth; month <= 12; month++)
            {
                // Check if there is data in the current month and year
                var found = rentalMonthSummaries.FirstOrDefault(r => r.Year == year.ToString() && r.Month == month.ToString());
                
                if (found is not null)
                {
                    fullRentalMonthSummaries.Add(found);

                    // If month and year are the final ones break the loop
                    if (month == finalMonth && year == finalYear)
                    {
                        break;
                    }

                    continue;
                }

                fullRentalMonthSummaries.Add(new RentalMonthSummaryDTO
                {
                    Month = month.ToString(),
                    Year = year.ToString(),
                    Amount = 0
                });

                // If month and year are the final ones break the loop
                if (month == finalMonth && year == finalYear)
                {
                    break;
                }
            }
        
            // Reset the initial month
            initialMonth = 1;
        }

        return fullRentalMonthSummaries;
    }

    public async Task<IEnumerable<RentalMonthRevenueDTO>> GetMonthlyRentalRevenue(DateOnly From, DateOnly To, bool IncludeNotReturned)
    {
        List<RentalMonthRevenueDTO> montlyRentalProfit = await db.GetMonthlyRentalRevenue(From, To, IncludeNotReturned)
            .Select(mrp => new RentalMonthRevenueDTO
            {
                Year = mrp.Year.ToString(),
                Month = mrp.Month.ToString(),
                Revenue = mrp.Revenue
            })
            .OrderBy(mrp => mrp.Year)
            .ThenBy(mrp => mrp.Month)
            .ToListAsync();

        return montlyRentalProfit;
    }
}