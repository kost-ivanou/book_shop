using Domain.Entities;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly IAppDbContext _context;

    public OrderRepository(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Order> GetByIdAsync(int id) => await _context.Orders.Include(o => o.Books).FirstOrDefaultAsync(o => o.Id == id);
    public async Task<IEnumerable<Order>> GetAllAsync() => await _context.Orders.Include(o => o.Books).ToListAsync();
    public async Task AddAsync(Order order) => await _context.Orders.AddAsync(order);
    public async Task DeleteAsync(Order order) => _context.Orders.Remove(order);
    public async Task SaveAsync() => await _context.SaveChangesAsync(CancellationToken.None);
}
