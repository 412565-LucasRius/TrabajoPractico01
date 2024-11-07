using CineRepository.Models.Entities;

namespace CineRepository.Models.DTO
  {
  public class MovieDTO
    {
    public int MovieId { get; set; }
    public string Title { get; set; }
    public DateTime? ReleaseDate { get; set; }
    public Genre Genre { get; set; }
    public Producer Producer { get; set; }

    public int? Duration { get; set; }
    public ICollection<ShowtimeDTO> Showtimes { get; set; }
    }
  }
