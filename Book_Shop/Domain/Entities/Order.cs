namespace Domain.Entities;

public class Order
{
    public int Id { get; set; }
    public DateTime OrderDate { get; set; }
    virtual public ICollection<Book>? Books { get; set; }
}
