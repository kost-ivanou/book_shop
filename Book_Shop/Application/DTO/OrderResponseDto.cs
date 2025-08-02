using Domain.Entities;

namespace Application.DTO;

public class OrderResponseDto
{
    public int Id { get; set; }
    public DateTime OrderDate { get; set; }
    virtual public ICollection<Book>? Books { get; set; }
}
