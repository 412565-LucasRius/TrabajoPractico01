namespace CineRepository.Models.DTO
  {
  public class BookingResponseDTO
    {
    public int? BookingId { get; set; }
    public int? CustomerId { get; set; }
    public DateTime? BookingDate { get; set; }
    public int? BookingStateId { get; set; }
    public List<TicketResponseDTO> Tickets { get; set; }
    public decimal TotalPrice { get; set; }
    }
  }
