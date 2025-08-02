using Application.DTO;
using Domain.Entities;

namespace Application.Interfaces;

public interface IBookService
{
    Task<IEnumerable<Book>> GetAllAsync();
    Task<Book> GetByIdAsync(int id);
    Task<int> CreateAsync(BookDto dto);
    Task<bool> DeleteAsync(int id);
    Task<IEnumerable<Book>> GetFilteredAsync(BookFilterDto filter);
}
