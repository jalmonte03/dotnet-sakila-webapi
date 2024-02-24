using Microsoft.EntityFrameworkCore;
using Sakila.App.WebAPI.Context;
using Sakila.App.WebAPI.DTOs;
using Sakila.App.WebAPI.Model;

namespace Sakila.App.WebAPI.Service;

public class FilmService : IFilmService
{
    SakilaContext db;
    private const int LIMIT = 10;
    

    public FilmService(SakilaContext db)
    {
        this.db = db;
    }

    public async Task<FilmGetDTO?> GetFilm(int id)
    {
        Film? film = await db.Films
            .Include(f => f.Categories)
            .FirstOrDefaultAsync(film => film.Id == id);

        if(film == null) 
        {
            return null;
        }

        return FilmGetDTO.MapFilmToDTO(film);
    }

    public async Task<FilmsGetDTO> GetFilms(int Page = 1, int Limit = 10, string Title = "")
    {
        var filmsQuery = db.Films
            .Include(f => f.Categories)
            .Where(f => f.Title!.Contains(Title));

        var filmsTotal = await filmsQuery.CountAsync();

        ICollection<Film> films = await filmsQuery
            .Skip((Page - 1) * Limit)
            .Take(Limit)
            .ToListAsync();

        var filmsDto = films.Select(FilmGetDTO.MapFilmToDTO);
        
        return new FilmsGetDTO
        {
            Page = Page,
            Total = filmsTotal,
            films = filmsDto
        };
    }

    public async Task<IEnumerable<RentalRentedInfoDTO>> GetMostRentedFilms(int Limit, DateOnly From, DateOnly To)
    {
        var rentalsQuery = from rental in db.Rentals
            join inventory in db.Inventories
                on rental.Inventory equals inventory
            join film in db.Films
                on inventory.Film equals film
            where DateOnly.FromDateTime(rental.RentalDate) > From &&
                 DateOnly.FromDateTime(rental.RentalDate) < To
            group film by film.Id into filmGroup
            select new RentalRentedInfoDTO() { 
                FilmId = filmGroup.Key,
                FilmTitle = filmGroup.First().Title!,
                Rented = filmGroup.Count()
            };
        
        var rentalRentedInfoDTO = await rentalsQuery
            .OrderByDescending(r => r.Rented)
            .ThenBy(r => r.FilmTitle)
            .Take(Limit)
            .ToListAsync();

        return rentalRentedInfoDTO;
    }

    public async Task<IEnumerable<CategoryRentedDTO>> GetMostWatchedCategories(int Limit, DateOnly From, DateOnly To)
    {
        IQueryable<CategoryRentedDTO> mostWatchedCategoriesQuery = from rental in db.Rentals
            join invetory in db.Inventories
                on rental.Inventory equals invetory
            join film in db.Films
                on invetory.Film equals film
            join film_category in db.FilmCategories
                on film.Id equals film_category.FilmId
            join category in db.Categories
                on film_category.Category equals category
            where DateOnly.FromDateTime(rental.RentalDate) > From &&
                 DateOnly.FromDateTime(rental.RentalDate) < To
            group category by category.Name into categoryGroup
            select new CategoryRentedDTO() {
                CategoryName = categoryGroup.Key,
                Amount = categoryGroup.Count()
            };

        var response = await mostWatchedCategoriesQuery
            .OrderByDescending(c => c.Amount)
            .Take(Limit)
            .ToListAsync();

        return response;
    }
}