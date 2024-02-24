using Sakila.App.WebAPI.DTOs;

namespace Sakila.App.WebAPI.Service;

public interface IRentalService
{
    public Task<RentalGetDTO?> GetRental(int Id);
    public Task<RentalsGetDTO> GetRentals(int Page, int Limit);
    public Task<IEnumerable<RentalMonthSummaryDTO>> GetMonthlyRentalsSummary(DateOnly From, DateOnly To);
    public Task<IEnumerable<RentalMonthRevenueDTO>> GetMonthlyRentalRevenue(DateOnly From, DateOnly To, bool IncludeNotReturned);
}