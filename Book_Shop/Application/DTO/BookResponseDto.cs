namespace Application.DTO;

public class BookResponseDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public double Price { get; set; }
    public DateTime Date { get; set; }
    public string? Author { get; set; }
}
