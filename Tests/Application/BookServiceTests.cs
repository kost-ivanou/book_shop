using Application.DTO;
using Application.Services;
using Domain.Entities;
using Infrastructure.Repository;
using Moq;

namespace Tests.Application;

public class BookServiceTests
{
    private readonly Mock<IBookRepository> _bookRepoMock;
    private readonly BookService _bookService;

    public BookServiceTests()
    {
        _bookRepoMock = new Mock<IBookRepository>();
        _bookService = new BookService(_bookRepoMock.Object);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAllBooks()
    {
        var books = new List<Book> { new Book { Name = "TestBook" } };
        _bookRepoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(books);

        var result = await _bookService.GetAllAsync();

        Assert.Equal(books, result);
    }

    [Fact]
    public async Task CreateAsync_ShouldAddBookAndSave()
    {
        var dto = new BookDto { Name = "NewBook", Author = "Author", Date = DateTime.Today, Price = 9.99 };

        await _bookService.CreateAsync(dto);

        _bookRepoMock.Verify(r => r.AddAsync(It.Is<Book>(b => b.Name == dto.Name)), Times.Once);
        _bookRepoMock.Verify(r => r.SaveAsync(), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_ShouldDeleteBook_WhenFound()
    {
        var book = new Book { Id = 1 };
        _bookRepoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(book);

        await _bookService.DeleteAsync(1);

        _bookRepoMock.Verify(r => r.DeleteAsync(book), Times.Once);
        _bookRepoMock.Verify(r => r.SaveAsync(), Times.Once);
    }
}
