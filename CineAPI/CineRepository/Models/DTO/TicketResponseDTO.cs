namespace CineRepository.Models.DTO
  {
  public class TicketResponseDTO
    {
    public int TicketId { get; set; }
    public string SeatNumber { get; set; }
    public DateTime SaleDate { get; set; }
    public DateTime ShowtimeStartDate { get; set; }
    public string MovieTitle { get; set; }
    public string CinemaName { get; set; }
    }
  }
