using Application.DTO;
using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Repositories;
using Infrastructure.Repository;

namespace Application.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepo;
    private readonly IBookRepository _bookRepo;

    public OrderService(IOrderRepository orderRepo, IBookRepository bookRepo)
    {
        _orderRepo = orderRepo;
        _bookRepo = bookRepo;
    }

    public async Task<IEnumerable<Order>> GetAllAsync() => await _orderRepo.GetAllAsync();

    public async Task<Order> GetByIdAsync(int id) => await _orderRepo.GetByIdAsync(id);

    public async Task CreateAsync(OrderDto dto)
    {
        var books = new List<Book>();
        foreach (var bookId in dto.BookIds)
        {
            var book = await _bookRepo.GetByIdAsync(bookId);
            if (book != null)
                books.Add(book);
        }

        var order = new Order
        {
            OrderDate = dto.OrderDate,
            Books = books
        };

        await _orderRepo.AddAsync(order);
        await _orderRepo.SaveAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var order = await _orderRepo.GetByIdAsync(id);
        if (order != null)
        {
            await _orderRepo.DeleteAsync(order);
            await _orderRepo.SaveAsync();
        }
    }

    public async Task<IEnumerable<Order>> GetFilteredAsync(OrderFilterDto filter)
    {
        var query = (await _orderRepo.GetAllAsync()).AsQueryable();

        if (filter.Id.HasValue)
            query = query.Where(o => o.Id == filter.Id.Value);

        if (filter.OrderDate.HasValue)
            query = query.Where(o => o.OrderDate.Date == filter.OrderDate.Value.Date);

        return query.ToList();
    }
}
