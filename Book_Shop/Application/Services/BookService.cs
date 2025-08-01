using Application.DTO;
using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Repository;

namespace Application.Services;

public class BookService : IBookService
{
    private readonly IBookRepository _repo;

    public BookService(IBookRepository repo)
    {
        _repo = repo;
    }

    public async Task<IEnumerable<Book>> GetAllAsync() => await _repo.GetAllAsync();

    public async Task<Book> GetByIdAsync(int id) => await _repo.GetByIdAsync(id);

    public async Task CreateAsync(BookDto dto)
    {
        var book = new Book
        {
            Name = dto.Name,
            Author = dto.Author,
            Date = dto.Date,
            Price = dto.Price
        };

        await _repo.AddAsync(book);
        await _repo.SaveAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var book = await _repo.GetByIdAsync(id);
        if (book != null)
        {
            await _repo.DeleteAsync(book);
            await _repo.SaveAsync();
        }
    }

    public async Task<IEnumerable<Book>> GetFilteredAsync(BookFilterDto filter)
    {
        var query = (await _repo.GetAllAsync()).AsQueryable();

        if (filter.Id.HasValue)
            query = query.Where(b => b.Id == filter.Id.Value);

        if (filter.Date.HasValue)
            query = query.Where(b => b.Date.Date == filter.Date.Value.Date);

        if (!string.IsNullOrEmpty(filter.Author))
            query = query.Where(b => b.Author != null && b.Author.Contains(filter.Author));

        return query.ToList();
    }
}
