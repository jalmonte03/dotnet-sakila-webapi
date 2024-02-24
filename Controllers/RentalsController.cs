using Microsoft.AspNetCore.Mvc;
using Sakila.App.WebAPI.Constants;
using Sakila.App.WebAPI.DTOs;
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

        if (rental == null)
        {
            return NotFound();
        } 

        return Ok(rental);
    }

    /// <summary>
    /// Get multiple rentals
    /// </summary>
    /// <param name="page"></param>
    /// <param name="limit"></param>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(200, Type = typeof(RentalsGetDTO))]
    public async Task<IActionResult> GetRentals(int page = 1, int limit = 10)
    {
        RentalsGetDTO rentals = await rentalService.GetRentals(page, limit);

        return Ok(rentals);
    }

    /// <summary>
    /// Get a monthly summary of rentals per month
    /// </summary>
    /// <param name="from">starting date</param>
    /// <param name="to">ending date</param>
    /// <returns></returns>
    [HttpGet("rentals_monthly_summary")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<RentalMonthSummaryDTO>))]
    public async Task<IActionResult> GetMonthlySummary(string from = SakilaConstant.START_RENT_DATE, string to = SakilaConstant.END_RENT_DATE)
    {
        if (!DateOnly.TryParse(from, out DateOnly fromDate))
        {
            return BadRequest("The from parameter is an invalid date");
        }
        
        if (!DateOnly.TryParse(to, out DateOnly toDate))
        {
            return BadRequest("The from parameter is an invalid date");
        }

        IEnumerable<RentalMonthSummaryDTO> rentalMonthSummaries = await rentalService.GetMonthlyRentalsSummary(fromDate, toDate);

        return Ok(rentalMonthSummaries);
    }

    /// <summary>
    /// Get the monthly revenue from rentals
    /// </summary>
    /// <param name="from">starting date</param>
    /// <param name="to">ending date</param>
    /// <param name="includeNotReturned">include rentals that hasn't be returned</param>
    /// <returns></returns>
    [HttpGet("monthly_rental_revenue")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<RentalMonthRevenueDTO>))]
    public async Task<IActionResult> GetMonthlyRentalRevenue(string from = SakilaConstant.START_RENT_DATE, string to = SakilaConstant.END_RENT_DATE, bool includeNotReturned = false)
    {
        if (!DateOnly.TryParse(from, out DateOnly fromDate))
        {
            return BadRequest("The from parameter is an invalid date");
        }
        
        if (!DateOnly.TryParse(to, out DateOnly toDate))
        {
            return BadRequest("The from parameter is an invalid date");
        }

        IEnumerable<RentalMonthRevenueDTO> rentalMonthlyProfitSummary = await rentalService.GetMonthlyRentalRevenue(fromDate, toDate, includeNotReturned);

        return Ok(rentalMonthlyProfitSummary);
    }
}