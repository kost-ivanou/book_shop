using Application.DTO;
using Domain.Entities;

namespace Application.Interfaces;

public interface IOrderService
{
    Task<IEnumerable<Order>> GetAllAsync();
    Task<Order> GetByIdAsync(int id);
    Task CreateAsync(OrderDto dto);
    Task DeleteAsync(int id);
    Task<IEnumerable<Order>> GetFilteredAsync(OrderFilterDto filter);
}
