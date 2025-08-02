using Application.DTO;
using Application.Services;
using Domain.Entities;
using Infrastructure.Repositories;
using Infrastructure.Repository;
using Moq;

namespace Tests.Application;

public class OrderServiceTests
{
    private readonly Mock<IOrderRepository> _orderRepoMock;
    private readonly Mock<IBookRepository> _bookRepoMock;
    private readonly OrderService _orderService;

    public OrderServiceTests()
    {
        _orderRepoMock = new Mock<IOrderRepository>();
        _bookRepoMock = new Mock<IBookRepository>();
        _orderService = new OrderService(_orderRepoMock.Object, _bookRepoMock.Object);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnOrder()
    {
        var order = new Order { Id = 1 };
        _orderRepoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(order);

        var result = await _orderService.GetByIdAsync(1);

        Assert.Equal(order, result);
    }

    [Fact]
    public async Task CreateAsync_ShouldCreateOrderWithBooks()
    {
        var dto = new OrderDto
        {
            BookIds = new List<int> { 1 },
            OrderDate = DateTime.Today
        };
        var book = new Book { Id = 1 };

        _bookRepoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(book);

        await _orderService.CreateAsync(dto);

        _orderRepoMock.Verify(r => r.AddAsync(It.Is<Order>(o => o.Books.Contains(book))), Times.Once);
        _orderRepoMock.Verify(r => r.SaveAsync(), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_ShouldDeleteOrder_WhenExists()
    {
        var order = new Order { Id = 1 };
        _orderRepoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(order);

        await _orderService.DeleteAsync(1);

        _orderRepoMock.Verify(r => r.DeleteAsync(order), Times.Once);
        _orderRepoMock.Verify(r => r.SaveAsync(), Times.Once);
    }
}
