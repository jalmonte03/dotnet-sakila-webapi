using Sakila.App.WebAPI.DTOs;

namespace Sakila.App.WebAPI.Service;

public interface IFilmService
{
    public Task<FilmGetDTO?> GetFilm(int id);
    public Task<FilmsGetDTO> GetFilms(int Page, int Limit);
    public Task<IEnumerable<RentalRentedInfoDTO>> GetMostRentedFilms(int Num, DateOnly From, DateOnly To);
}