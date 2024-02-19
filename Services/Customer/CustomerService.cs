
using Microsoft.EntityFrameworkCore;
using Sakila.App.WebAPI.Context;
using Sakila.App.WebAPI.DTOs;
using Sakila.App.WebAPI.Model;

namespace Sakila.App.WebAPI.Service;

public class CustomerService : ICustomerService
{
    SakilaContext db;

    public CustomerService(SakilaContext db)
    {
        this.db = db;
    }

    public async Task<CustomerDTO?> GetCustomer(int Id)
    {
        Customer? c = await db.Customers
                        .Include(c => c.Address)
                        .ThenInclude(a => a.City)
                        .ThenInclude(c => c.Country)
                        .FirstOrDefaultAsync(customer => customer.Id == Id);
                        
        if (c == null)
        {
            return null;
        }

        return CustomerDTO.MapCustomerToDTO(c);
    }

    public async Task<CustomersResponseDTO> GetCustomers(int Page, int Limit, string Name)
    {
        var query = db.Customers
            .Include(c => c.Address)
            .ThenInclude(a => a.City)
            .ThenInclude(c => c.Country)
            .Where(c => (c.FirstName + ' ' + c.LastName).Contains(Name));

        int totalCustomers = await query.CountAsync();
        ICollection<Customer> customers = await query
            .Skip((Page - 1) * Limit)
            .Take(Limit)
            .ToListAsync();

        return new CustomersResponseDTO()
        {
            CurrentPage = Page,
            Customers = customers.Select(c => CustomerDTO.MapCustomerToDTO(c)),
            Total = totalCustomers
        };
    }

    public async Task<CustomerRentalsDTO> GetCustomerRentals(int Id, int Page, int Limit)
    {
        int Count = await db.Rentals
            .Where(rental => rental.CustomerId == Id)
            .CountAsync();
        int TotalPages = (int)Math.Ceiling(Count/((double)Limit));

        IEnumerable<Rental> rentals = await db.Rentals
            .Include(r => r.Customer)
            .Include(r => r.Inventory)
            .ThenInclude(i => i.Film)
            .Where(rental => rental.CustomerId == Id)
            .Skip((Page - 1) * Limit)
            .Take(Limit)
            .ToListAsync();
        
        return new CustomerRentalsDTO(){
            Page = Page,
            Total = TotalPages,
            Rentals = rentals.Select(CustomerRentalGetDTO.MapRentalToDTO)
        };
    }

    public async Task<IEnumerable<CustomerWatchedCategoryDTO>> GetCustomerWatchedCategories(int Id)
    {
        var rentals = 
            from rental in db.Rentals
            join inventory in db.Inventories
                on rental.Inventory equals inventory
            join film in db.Films
                on inventory.Film equals film
            join film_category in db.FilmCategories
                on film.Id equals film_category.FilmId
            join category in db.Categories
                on film_category.Category equals category
            where rental.CustomerId == Id
            group category by category.Name into g
            select new {
                Name = g.Key,
                MoviesWatched = g.Count()
            };

        var categoriesGroup = await rentals.OrderByDescending(c => c.MoviesWatched).ToListAsync();
        
        return categoriesGroup.Select(cat => new CustomerWatchedCategoryDTO(){
            Category = cat.Name,
            MoviesWatched = (ushort)cat.MoviesWatched
        });
    }

    public async Task<CustomerSummaryDTO?> GetCustomerSummary(int Id)
    {
        // Get the Name
        var Customer = await GetCustomer(Id);

        if (Customer == null)
        {
            return null;
        } 
        
        var MostViewedCategory = (await GetCustomerWatchedCategories(Id)).FirstOrDefault();
        
        var MoviesRented = await GetCustomerRentals(Id, Page: 1, Limit: int.MaxValue);  
        var MoviesRentedTotal = MoviesRented.Rentals.Count();
        var TotalSpent = MoviesRented.Rentals.Sum(rental => rental.FilmRentalRate);

        return new CustomerSummaryDTO()
        {
            Id = Id,
            CustomerName = $"{Customer.First_Name} {Customer.Last_Name}",
            MostViewedCategory = MostViewedCategory?.Category,
            MoviesRentedTotal = MoviesRentedTotal,
            TotalSpent = TotalSpent
        };
    }
}