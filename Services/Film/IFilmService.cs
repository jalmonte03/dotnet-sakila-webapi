using Sakila.App.WebAPI.DTOs;

namespace Sakila.App.WebAPI.Service;

public interface IFilmService
{
    public Task<FilmGetDTO?> GetFilm(int id);
    public Task<FilmsGetDTO> GetFilms(int Page, int Limit, string Title);
    public Task<IEnumerable<RentalRentedInfoDTO>> GetMostRentedFilms(int Num, DateOnly From, DateOnly To);
    public Task<IEnumerable<CategoryRentedDTO>> GetMostWatchedCategories(int CategoryNum, DateOnly From, DateOnly To);
    public Task<FilmSummaryDTO?> GetFilmSummary(int id);
}