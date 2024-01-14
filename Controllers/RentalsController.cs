using Microsoft.AspNetCore.Mvc;
using Sakila.App.WebAPI.DTOs;
using Sakila.App.WebAPI.Model;
using Sakila.App.WebAPI.Service;

namespace Sakila.App.WebAPI.Controller;

[ApiController]
[Route("[controller]")]
public class RentalsController : ControllerBase
{
    IRentalService rentalService;

    public RentalsController(IRentalService rentalService)
    {
        this.rentalService = rentalService;
    }

    /// <summary>
    /// Get a single rental
    /// </summary>
    /// <param name="id">The id to search</param>
    /// <returns></returns>
    [HttpGet("{id:int}")]
    [ProducesResponseType(200, Type = typeof(RentalGetDTO))]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetRental(int id)
    {
        RentalGetDTO? rental = await rentalService.GetRental(id);

        if (rental == null) return NotFound();

        return Ok(rental);
    }

    /// <summary>
    /// Get multiple rentals
    /// </summary>
    /// <param name="Page"></param>
    /// <param name="Limit"></param>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<RentalGetDTO>))]
    public async Task<IActionResult> GetRentals(int Page, int Limit)
    {
        IEnumerable<RentalGetDTO> rentals = await rentalService.GetRentals(Page, Limit);

        return Ok(rentals);
    }
}