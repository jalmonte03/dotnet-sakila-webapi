using Microsoft.AspNetCore.Mvc;
using Sakila.App.WebAPI.DTOs;
using Sakila.App.WebAPI.Model;

namespace Sakila.App.WebAPI.Service;

public interface IRentalService
{
    public Task<RentalGetDTO?> GetRental(int Id);
    public Task<IEnumerable<RentalGetDTO>> GetRentals(int Page, int Limit);
}