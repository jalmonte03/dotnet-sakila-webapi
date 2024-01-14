using Sakila.App.WebAPI.Model;

public class RentalRentedInfoDTO
{
    public int FilmId { get; set; }
    public string FilmTitle { get; set; } = "";
    public int Rented { get; set; }
}