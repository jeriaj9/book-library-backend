using Library.Application.Interfaces;
using Library.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Controllers;

[ApiController]
[Route("api/books")]
public class BooksController : ControllerBase
{
    private readonly IBookService _bookService;

    public BooksController(IBookService bookService)
    {
        _bookService = bookService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Book>>> GetAll()
    {
        var books = await _bookService.GetAllAsync();
        return Ok(books);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Book>> GetById(int id)
    {
        var book = await _bookService.GetByIdAsync(id);

        if (book is null)
            return NotFound();

        return Ok(book);
    }

    [HttpPost]
    public async Task<ActionResult<Book>> Create(Book book)
    {
        var created = await _bookService.CreateAsync(book);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<Book>> Update(int id, Book book)
    {
        var updated = await _bookService.UpdateAsync(id, book);
        return Ok(updated);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _bookService.DeleteAsync(id);
        return NoContent();
    }
}
