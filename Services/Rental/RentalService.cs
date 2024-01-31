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
}