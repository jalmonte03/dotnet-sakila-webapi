using Sakila.App.WebAPI.Model;

namespace Sakila.App.WebAPI.DTOs;

public class CustomerDTO
{
    public int Id { get; set; }
    
    public string? First_Name { get; set; }

    public string? Last_Name { get; set; }

    public string? Email { get; set; }
   
    public char Active { get; set; }

    public string? StreetAddress { get; set; }

    public string? StreetAddress2 { get; set; }

    public string? City { get; set; }

    public string? Country { get; set; }

    public String? Zipcode { get; set; }

    public DateTime Created { get; set; }

    public static CustomerDTO MapCustomerToDTO (Customer c)
    {
        return new CustomerDTO()
        {
            Id = c.Id,
            First_Name = c.FirstName,
            Last_Name = c.LastName,
            Active = c.Active,
            StreetAddress = c.Address.StreetAddress,
            StreetAddress2 = c.Address.StreetAddress2,
            City = c.Address.City.CityName,
            Country = c.Address.City.Country.CountryName,
            Email = c.Email,
            Created = c.CreateDate
        };
    }
}