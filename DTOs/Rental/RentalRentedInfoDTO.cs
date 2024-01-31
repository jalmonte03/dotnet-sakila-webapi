using Sakila.App.WebAPI.Model;

namespace Sakila.App.WebAPI.DTOs;

public class RentalRentedInfoDTO
{
    public int FilmId { get; set; }
    public string FilmTitle { get; set; } = "";
    public int Rented { get; set; }
}