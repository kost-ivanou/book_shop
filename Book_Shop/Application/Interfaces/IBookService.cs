using Application.DTO;
using Domain.Entities;

namespace Application.Interfaces;

public interface IBookService
{
    Task<IEnumerable<Book>> GetAllAsync();
    Task<Book> GetByIdAsync(int id);
    Task CreateAsync(BookDto dto);
    Task DeleteAsync(int id);
    Task<IEnumerable<Book>> GetFilteredAsync(BookFilterDto filter);
}
