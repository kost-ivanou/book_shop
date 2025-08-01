using Application.DTO;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/books")]
public class BookController : ControllerBase
{
    private readonly IBookService _bookService;

    public BookController(IBookService bookService)
    {
        _bookService = bookService;
    }

    [HttpGet]
    public async Task<IActionResult> GetBooks() => Ok(await _bookService.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetBookById([FromRoute] int id)
    {
        var book = await _bookService.GetByIdAsync(id);
        if (book == null)
            return NotFound();

        return Ok(book);
    }

    [HttpPost]
    public async Task<IActionResult> CreateBook([FromBody] BookDto dto)
    {
        await _bookService.CreateAsync(dto);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBook(int id)
    {
        await _bookService.DeleteAsync(id);
        return Ok();
    }

    [HttpPost("filter")]
    public async Task<IActionResult> FilterBooks([FromBody] BookFilterDto filter)
    {
        var result = await _bookService.GetFilteredAsync(filter);
        return Ok(result);
    }
}
