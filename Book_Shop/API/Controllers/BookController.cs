using Application.DTO;
using Application.Interfaces;
using Domain.Entities;
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
    public async Task<ActionResult<List<BookResponseDto>>> GetBooks()
    {
        var books = await _bookService.GetAllAsync();
        return Ok(books);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BookResponseDto>> GetBookById([FromRoute] int id)
    {
        var book = await _bookService.GetByIdAsync(id);
        if (book == null)
            return NotFound();

        return Ok(book);
    }

    [HttpPost]
    public async Task<ActionResult<int>> CreateBook([FromBody] BookDto dto)
    {
        int id = await _bookService.CreateAsync(dto);
        return Ok(id);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBook(int id)
    {
        if(!await _bookService.DeleteAsync(id))
        {
            return NotFound();
        }
        return Ok();
    }

    [HttpGet("filter")]
    public async Task<ActionResult<List<BookResponseDto>>> FilterBooks([FromQuery] BookFilterDto filter)
    {
        var result = await _bookService.GetFilteredAsync(filter);
        return Ok(result);
    }
}
