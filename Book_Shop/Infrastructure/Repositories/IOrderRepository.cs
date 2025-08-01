using Domain.Entities;

namespace Infrastructure.Repositories;

public interface IOrderRepository
{
    Task<Order> GetByIdAsync(int id);
    Task<IEnumerable<Order>> GetAllAsync();
    Task AddAsync(Order order);
    Task DeleteAsync(Order order);
    Task SaveAsync();
}
