namespace CineRepository.Models.DTO
  {
  public class BookingWithTicketsRequest
    {
    public BookingRequest BookingRequest { get; set; }
    public List<TicketRequest> TicketRequest { get; set; }
    }

  }
