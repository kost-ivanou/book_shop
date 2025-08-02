using API.Controllers;
using Application.DTO;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Tests.Controllers;

public class BookControllerTests
{
    private readonly Mock<IBookService> _bookServiceMock;
    private readonly BookController _controller;

    public BookControllerTests()
    {
        _bookServiceMock = new Mock<IBookService>();
        _controller = new BookController(_bookServiceMock.Object);
    }

    [Fact]
    public async Task GetBookById_ReturnsNotFound_WhenBookIsNull()
    {
        _bookServiceMock.Setup(s => s.GetByIdAsync(1)).ReturnsAsync((Book)null);

        var result = await _controller.GetBookById(1);

        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task CreateBook_ReturnsOk()
    {
        var dto = new BookDto { Name = "Test", Author = "A", Date = DateTime.Today, Price = 10 };

        var result = await _controller.CreateBook(dto);

        _bookServiceMock.Verify(s => s.CreateAsync(dto), Times.Once);
        Assert.IsType<OkResult>(result);
    }
}
