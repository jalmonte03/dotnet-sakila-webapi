namespace Sakila.App.WebAPI.DTOs;

public class FilmSummaryDTO
{
    public int Rented { get; set; }
    public int NotReturned { get; set; }
    public decimal GrossIncome { get; set; }
    public int InStock { get; set; }
}

public class FilmSummaryQueryDTO 
{
    public int RentalId { get; set; }
    public DateTime? ReturnedDate { get; set; }
    public decimal GrossIncome { get; set; }
}