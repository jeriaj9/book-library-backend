using Library.Application.Interfaces;
using Library.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Controllers;

[ApiController]
[Route("api/authors")]
public class AuthorsController : ControllerBase
{
    private readonly IAuthorService _authorService;

    public AuthorsController(IAuthorService authorService)
    {
        _authorService = authorService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Author>>> GetAll()
    {
        var authors = await _authorService.GetAllAsync();
        return Ok(authors);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Author>> GetById(int id)
    {
        var author = await _authorService.GetByIdAsync(id);

        if (author is null)
            return NotFound();

        return Ok(author);
    }

    [HttpPost]
    public async Task<ActionResult<Author>> Create(Author author)
    {
        var created = await _authorService.CreateAsync(author);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<Author>> Update(int id, Author author)
    {
        var updated = await _authorService.UpdateAsync(id, author);
        return Ok(updated);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _authorService.DeleteAsync(id);
        return NoContent();
    }
}
