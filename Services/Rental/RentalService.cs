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

        return RentalMonthSummaryDTO.FillMissingMonths(rentalMonthSummaries, From, To);
    }

    public async Task<IEnumerable<RentalMonthRevenueDTO>> GetMonthlyRentalRevenue(DateOnly From, DateOnly To, bool IncludeNotReturned)
    {
        List<RentalMonthRevenueDTO> monthlyRentalProfit = await db.GetMonthlyRentalRevenue(From, To, IncludeNotReturned)
            .Select(mrp => new RentalMonthRevenueDTO
            {
                Year = mrp.Year.ToString(),
                Month = mrp.Month.ToString(),
                Revenue = mrp.Revenue
            })
            .Where(mrp => mrp.Revenue != 0)
            .OrderBy(mrp => mrp.Year)
            .ThenBy(mrp => mrp.Month)
            .ToListAsync();

        if(IncludeNotReturned)
        {
            return RentalMonthRevenueDTO.FillMissingMonths(monthlyRentalProfit, From, To);
        }
        
        return monthlyRentalProfit;
    }
}