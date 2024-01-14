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

        if(film == null) return null;

        return FilmGetDTO.MapFilmToDTO(film);
    }

    public async Task<IEnumerable<FilmGetDTO>> GetFilms(int Page = 1, int Limit = 10)
    {
        ICollection<Film> films = await db.Films
            .Include(f => f.Categories)
            .Skip((Page - 1) * Limit)
            .Take(Limit)
            .ToListAsync();

        return films.Select(FilmGetDTO.MapFilmToDTO);
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
            select new { 
                FilmId = filmGroup.Key,
                FilmTitle = filmGroup.First().Title,
                Rented = filmGroup.Count()
            };
        
        var response = await rentalsQuery
            .OrderByDescending(r => r.Rented)
            .ThenBy(r => r.FilmTitle)
            .Take(Limit)
            .ToListAsync();
        
        IEnumerable<RentalRentedInfoDTO> rentalRentedInfoDTO = response.Select(r => new RentalRentedInfoDTO()
        {
            FilmId = r.FilmId,
            FilmTitle = r.FilmTitle,
            Rented = r.Rented
        });

        return rentalRentedInfoDTO;
    }
}