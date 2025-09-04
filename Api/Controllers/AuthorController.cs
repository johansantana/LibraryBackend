using Application.Interfaces;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthorController : ControllerBase
{
    private readonly IAuthorService _service;

    public AuthorController(IAuthorService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Author>>> GetAll()
    {
        var authors = await _service.GetAllAsync();
        return Ok(authors);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Author>> GetById(int id)
    {
        var author = await _service.GetByIdAsync(id);
        return author is null ? NotFound() : Ok(author);
    }

    [HttpPost]
    public async Task<ActionResult<Author>> Create(Author author)
    {
        var createdAuthor = await _service.CreateAsync(author);
        return CreatedAtAction(nameof(GetById), new { id = createdAuthor.Id }, createdAuthor);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Author>> Update(int id, Author author)
    {
        var updatedAuthor = await _service.UpdateAsync(id, author);
        return updatedAuthor is null ? NotFound() : Ok(updatedAuthor);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _service.DeleteAsync(id);
        return deleted ? NoContent() : NotFound();
    }
}
