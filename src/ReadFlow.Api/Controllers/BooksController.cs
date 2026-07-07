using Microsoft.AspNetCore.Mvc;
using ReadFlow.Api.Requests;
using ReadFlow.Application.Interfaces;

namespace ReadFlow.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private readonly IBookService _bookService;
    private readonly IReadingNoteService _readingNoteService;

    public BooksController(
        IBookService bookService,
        IReadingNoteService readingNoteService)
    {
        _bookService = bookService;
        _readingNoteService = readingNoteService;
    }

    [HttpGet]
    public async Task<IActionResult> GetBooks()
    {
        var books = await _bookService.GetAllAsync();
        return Ok(books);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetBookById(int id)
    {
        var book = await _bookService.GetByIdAsync(id);

        if (book == null)
        {
            return NotFound("Book not found.");
        }

        return Ok(book);
    }

    [HttpPost]
    public async Task<IActionResult> CreateBook(CreateBookRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Title))
        {
            return BadRequest("Title is required.");
        }

        if (string.IsNullOrWhiteSpace(request.Author))
        {
            return BadRequest("Author is required.");
        }

        var book = await _bookService.CreateAsync(request.Title, request.Author, request.Year);

        return CreatedAtAction(nameof(GetBookById), new { id = book.Id }, book);
    }

    [HttpPatch("{id}/status")]
    public async Task<IActionResult> UpdateBookStatus(int id, UpdateBookStatusRequest request)
    {
        try
        {
            var book = await _bookService.UpdateStatusAsync(id, request.Status);

            if (book == null)
            {
                return NotFound("Book not found.");
            }

            return Ok(book);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id}/notes")]
    public async Task<IActionResult> GetNotes(int id)
    {
        var notes = await _readingNoteService.GetByBookIdAsync(id);

        if (notes == null)
        {
            return NotFound("Book not found.");
        }

        return Ok(notes);
    }

    [HttpPost("{id}/notes")]
    public async Task<IActionResult> CreateNote(int id, CreateReadingNoteRequest request)
    {
        try
        {
            var note = await _readingNoteService.CreateAsync(id, request.Content);

            if (note == null)
            {
                return NotFound("Book not found.");
            }

            return CreatedAtAction(nameof(GetNotes), new { id = id }, note);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
