namespace Domain.Entities;

public class Book
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public double Price { get; set; }
    public DateTime Date { get; set; }
    public string? Author { get; set; }
}
