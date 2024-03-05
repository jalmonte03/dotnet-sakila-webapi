namespace Sakila.App.WebAPI.DTOs;

public class RentalMonthSummaryDTO 
{
    public string Month { get; set; } = null!;
    public string Year { get; set; } = null!;
    public int Amount { get; set; }

    public static List<RentalMonthSummaryDTO> FillMissingMonths(List<RentalMonthSummaryDTO> list, DateOnly From, DateOnly To)
    {
        // Get the date range
        int initialMonth = From.Month;
        int initialYear = From.Year;
        int finalMonth = To.Month;
        int finalYear = To.Year;

        // The full list covering all months
        List<RentalMonthSummaryDTO> fullRentalMonthSummaries = new List<RentalMonthSummaryDTO>();

        // Rebuild the full rental summary including empty months
        for(int year = initialYear; year <= finalYear; year++)
        {
            for(int month = initialMonth; month <= 12; month++)
            {
                // Check if there is data in the current month and year
                var found = list.FirstOrDefault(r => r.Year == year.ToString() && r.Month == month.ToString());
                
                if (found is not null)
                {
                    fullRentalMonthSummaries.Add(found);

                    // If month and year are the final ones break the loop
                    if (month == finalMonth && year == finalYear)
                    {
                        break;
                    }

                    continue;
                }

                fullRentalMonthSummaries.Add(new RentalMonthSummaryDTO
                {
                    Month = month.ToString(),
                    Year = year.ToString(),
                    Amount = 0
                });

                // If month and year are the final ones break the loop
                if (month == finalMonth && year == finalYear)
                {
                    break;
                }
            }
        
            // Reset the initial month
            initialMonth = 1;
        }

        return fullRentalMonthSummaries;
    }
}