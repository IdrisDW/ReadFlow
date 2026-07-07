using Microsoft.AspNetCore.Mvc;
using ReadFlow.Api.Requests;
using ReadFlow.Application.Interfaces;

namespace ReadFlow.Api.Controllers;

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
    public async Task<IActionResult> GetAll()
    {
        var books = await _bookService.GetAllAsync();

        return Ok(books);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var book = await _bookService.GetByIdAsync(id);

        if (book == null)
        {
            return NotFound();
        }

        return Ok(book);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateBookRequest request)
    {
        try
        {
            var book = await _bookService.CreateAsync(request.Title, request.Author);

            return CreatedAtAction(
                nameof(GetById),
                new { id = book.Id },
                book);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPatch("{id}/status")]
    public async Task<IActionResult> UpdateStatus(int id, UpdateBookStatusRequest request)
    {
        try
        {
            var book = await _bookService.UpdateStatusAsync(id, request.Status);

            if (book == null)
            {
                return NotFound();
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
        var notes = await _bookService.GetNotesAsync(id);

        if (notes == null)
        {
            return NotFound();
        }

        return Ok(notes);
    }

    [HttpPost("{id}/notes")]
    public async Task<IActionResult> AddNote(int id, CreateReadingNoteRequest request)
    {
        try
        {
            var note = await _bookService.AddNoteAsync(id, request.Content);

            if (note == null)
            {
                return NotFound();
            }

            return Ok(note);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPatch("{id}/rating")]
    public async Task<IActionResult> UpdateRating(int id, UpdateBookRatingRequest request)
    {
        try
        {
            var book = await _bookService.UpdateRatingAsync(id, request.Rating);

            if (book == null)
            {
                return NotFound();
            }

            return Ok(book);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}