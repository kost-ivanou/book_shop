using API.Controllers;
using Application.DTO;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Tests.Controllers;

public class OrderControllerTests
{
    private readonly Mock<IOrderService> _orderServiceMock;
    private readonly OrderController _controller;

    public OrderControllerTests()
    {
        _orderServiceMock = new Mock<IOrderService>();
        _controller = new OrderController(_orderServiceMock.Object);
    }

    [Fact]
    public async Task GetOrders_ReturnsOkWithOrders()
    {
        var orders = new List<Order> { new Order { Id = 1 } };
        _orderServiceMock.Setup(s => s.GetAllAsync()).ReturnsAsync(orders);

        var result = await _controller.GetOrders();

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(orders, okResult.Value);
    }

    [Fact]
    public async Task GetOrderById_ReturnsNotFound_WhenNull()
    {
        _orderServiceMock.Setup(s => s.GetByIdAsync(1)).ReturnsAsync((Order)null);

        var result = await _controller.GetOrderById(1);

        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task CreateOrder_ReturnsOk()
    {
        var dto = new OrderDto { BookIds = new List<int> { 1 }, OrderDate = DateTime.Today };

        var result = await _controller.CreateOrder(dto);

        _orderServiceMock.Verify(s => s.CreateAsync(dto), Times.Once);
        Assert.IsType<OkResult>(result);
    }
}
