using Microsoft.AspNetCore.Mvc;
using Sakila.App.WebAPI.DTOs;
using Sakila.App.WebAPI.Service;
using Sakila.App.WebAPI.Constants;

namespace Sakila.App.WebAPI.Controller;

[ApiController]
[Route("[controller]")]
public class FilmsController : ControllerBase
{
    IFilmService filmService;    

    public FilmsController(IFilmService filmService)
    {
        this.filmService = filmService;
    }

    /// <summary>
    /// Get a single film
    /// </summary>
    /// <param name="id">The id to search</param>
    /// <returns></returns>
    [HttpGet("{id:int}")]
    [ProducesResponseType(200, Type = typeof(FilmGetDTO))]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetFilm(int id)
    {
        FilmGetDTO? film = await filmService.GetFilm(id);

        if (film == null)
        {
            return NotFound();
        } 
        
        return Ok(film);
    }

    /// <summary>
    /// Get multiple films.
    /// </summary>
    /// <param name="page">Current Page to search</param>
    /// <param name="limit">Limit of films per page</param>
    /// <param name="title">The title of the film to seach</param>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(200, Type = typeof(FilmsGetDTO))]
    public async Task<IActionResult> GetAllFilms(int page = 1, int limit = 10, string title = "")
    {
        return Ok(await filmService.GetFilms(page, limit, title));
    }

    /// <summary>
    /// Get most rented films from a date range, and limit the results
    /// </summary>
    /// <param name="limit">how many films to show</param>
    /// <param name="from">starting date</param>
    /// <param name="to">ending date</param>
    /// <returns></returns>
    [HttpGet("most_rented_films")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<RentalRentedInfoDTO>))]
    [ProducesResponseType(400, Type = typeof(string))]
    public async Task<IActionResult> GetMostRentedFilms(int limit = 3, string from = SakilaConstant.START_RENT_DATE, string to = SakilaConstant.END_RENT_DATE)
    {
        if (limit <= 0)
        {
            return BadRequest("The limit parameter can't be zero or a negative number.");
        }   

        if (limit > 100)
        {
            return BadRequest("The limit parameter can't be more than a 100.");
        }

        if (!DateOnly.TryParse(from, out DateOnly fromDate))
        {
            return BadRequest("The from parameter is an invalid date");
        }
        
        if (!DateOnly.TryParse(to, out DateOnly toDate))
        {
            return BadRequest("The from parameter is an invalid date");
        }

        return Ok(await filmService.GetMostRentedFilms(limit, fromDate, toDate));
    }

    /// <summary>
    /// Get the most watched categories
    /// </summary>
    /// <param name="limit">How many categories to show</param>
    /// <param name="from">starting date</param>
    /// <param name="to">ending date</param>
    /// <returns></returns>
    [HttpGet("most_watched_categories")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<CategoryRentedDTO>))]
    public async Task<IActionResult> GetMostWatchedCategories(int limit = 3, string from = SakilaConstant.START_RENT_DATE, string to = SakilaConstant.END_RENT_DATE)
    {
        if (limit <= 0)
        {
            return BadRequest("The limit parameter can't be zero or a negative number.");
        }

        if (!DateOnly.TryParse(from, out DateOnly fromDate))
        {
            return BadRequest("The from parameter is an invalid date");
        }
        
        if (!DateOnly.TryParse(to, out DateOnly toDate))
        {
            return BadRequest("The from parameter is an invalid date");
        }

        return Ok(await filmService.GetMostWatchedCategories(limit, fromDate, toDate));
    }
}