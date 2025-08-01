using Domain.Entities;

namespace Infrastructure.Repository;

public interface IBookRepository
{
    Task<Book> GetByIdAsync(int id);
    Task<IEnumerable<Book>> GetAllAsync();
    Task AddAsync(Book book);
    Task DeleteAsync(Book book);
    Task SaveAsync();
}
