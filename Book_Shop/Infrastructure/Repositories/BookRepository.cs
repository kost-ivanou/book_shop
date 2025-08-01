using Domain.Entities;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;

public class BookRepository : IBookRepository
{
    private readonly IAppDbContext _context;

    public BookRepository(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Book> GetByIdAsync(int id) => await _context.Books.FindAsync(id);
    public async Task<IEnumerable<Book>> GetAllAsync() => await _context.Books.ToListAsync();
    public async Task AddAsync(Book book) => await _context.Books.AddAsync(book);
    public async Task DeleteAsync(Book book) => _context.Books.Remove(book);
    public async Task SaveAsync() => await _context.SaveChangesAsync(CancellationToken.None);
}
