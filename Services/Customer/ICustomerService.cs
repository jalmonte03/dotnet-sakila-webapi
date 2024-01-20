using Sakila.App.WebAPI.DTOs;

namespace Sakila.App.WebAPI.Service;

public interface ICustomerService
{
    public Task<CustomerDTO?> GetCustomer(int Id);
    public Task<CustomersResponseDTO> GetCustomers(int Page, int Limit);
    public Task<CustomerRentalsDTO> GetCustomerRentals(int Id, int Page, int Limit);
    public Task<IEnumerable<CustomerWatchedCategoryDTO>> GetCustomerWatchedCategories(int Id);
    public Task<CustomerSummaryDTO?> GetCustomerSummary(int Id);
}