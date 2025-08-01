namespace Application.DTO;

public class OrderDto
{
    public DateTime OrderDate { get; set; }
    public List<int>? BookIds { get; set; }
}
