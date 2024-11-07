namespace CineRepository.Models.DTO
  {
  public class TicketRequest
    {
    public int ShowtimeId { get; set; }
    public string SeatNumber { get; set; }
    public DateTime SaleDate { get; set; }
    public decimal Price { get; set; }
    }

  }
