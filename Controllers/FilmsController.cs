using Microsoft.AspNetCore.Mvc;
using Sakila.App.WebAPI.DTOs;
using Sakila.App.WebAPI.Model;
using Sakila.App.WebAPI.Service;

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
    /// <param name="p">Current Page to search</param>
    /// <param name="Limit">Limit of films per page</param>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(200, Type = typeof(FilmsGetDTO))]
    public async Task<IActionResult> GetAllFilms(int p = 1, int Limit = 10)
    {
        return Ok(await filmService.GetFilms(p, Limit));
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
    public async Task<IActionResult> GetMostRentedFilms(int limit = 3, string from = "2005-05-24", string to="2005-05-28")
    {
        if (limit <= 0)
        {
            return BadRequest("The limit parameter can't be zero or a negative number.");
        }   

        if (limit > 100)
        {
            return BadRequest("The limit parameter can't be more than a 100.");
        }

        return Ok(await filmService.GetMostRentedFilms(limit, DateOnly.Parse(from), DateOnly.Parse(to)));
    }
}