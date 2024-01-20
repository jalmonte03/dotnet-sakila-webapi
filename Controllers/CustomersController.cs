using Microsoft.AspNetCore.Mvc;
using Sakila.App.WebAPI.DTOs;
using Sakila.App.WebAPI.Model;
using Sakila.App.WebAPI.Service;

namespace Sakila.App.WebAPI.Controller;

[ApiController]
[Route("[controller]")]
public class CustomersController : ControllerBase
{
    private ICustomerService customerService;

    public CustomersController(ICustomerService customerService)
    {
        this.customerService = customerService;
    }

    /// <summary>
    /// Get a single customer by Id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id:int}")]
    [ProducesResponseType(200, Type = typeof(CustomerDTO))]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetCustomer(int id)
    {
        CustomerDTO? customer = await customerService.GetCustomer(id);

        if (customer == null) return NotFound();

        return Ok(customer);
    }

    /// <summary>
    /// Get multiple customers.
    /// </summary>
    /// <param name="page">The current page being viewed</param>
    /// <param name="limit">The amount of customer per page</param>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(200, Type = typeof(CustomersResponseDTO))]
    [ProducesResponseType(400, Type = typeof(string))]
    public async Task<IActionResult> GetCustomers(int page = 1, int limit = 10)
    {
        if (page <= 0 || limit <= 0) return BadRequest("Page and Limit must be greater than 0");

        return Ok(await customerService.GetCustomers(page, limit));
    }


    /// <summary>
    /// Get customer's movies rentals
    /// </summary>
    /// <param name="id">Customer Id</param>
    /// <param name="page">Current page to view</param>
    /// <param name="limit">Movies rental per page</param>
    /// <returns></returns>
    [HttpGet("{id:int}/rentals")]
    [ProducesResponseType(200, Type = typeof(CustomerRentalsDTO))]
    public async Task<IActionResult> GetCustomersRentals(int id, int page = 1, int limit = 10)
    {
        if (page <= 0 || limit <= 0) return BadRequest("Page and Limit must be greater than 0");

        return Ok(await customerService.GetCustomerRentals(id, page, limit));
    }

    /// <summary>
    /// Get customer watched movies categories
    /// </summary>
    /// <param name="id">Customer Id</param>
    /// <returns></returns>
    [HttpGet("{id:int}/watched_categories")]
    [ProducesResponseType(200, Type = typeof(CustomerWatchedCategoryDTO))]
    public async Task<IActionResult> GetCustomerWatchedCategories(int id)
    {
        return Ok(await customerService.GetCustomerWatchedCategories(id));
    }

    /// <summary>
    /// Get a summary from a customer
    /// </summary>
    /// <param name="id">The customer Id</param>
    /// <returns></returns>
    [HttpGet("{id:int}/summary")]
    [ProducesResponseType(200, Type = typeof(CustomerSummaryDTO))]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetCustomerSummary(int id)
    {
        CustomerSummaryDTO? summary = await customerService.GetCustomerSummary(id);

        if (summary == null) 
            return NotFound();

        return Ok(summary);
    }
}

