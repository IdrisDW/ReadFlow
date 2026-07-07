using Microsoft.AspNetCore.Mvc;
using ReadFlow.Application.Interfaces;
using ReadFlow.Application.Requests;

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

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var book = await _bookService.GetByIdAsync(id);

        if (book is null)
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
            var book = await _bookService.CreateAsync(request);

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

    [HttpPatch("{id:int}/status")]
    public async Task<IActionResult> UpdateStatus(int id, UpdateBookStatusRequest request)
    {
        try
        {
            var book = await _bookService.UpdateStatusAsync(id, request);

            if (book is null)
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

    [HttpGet("{id:int}/status-history")]
    public async Task<IActionResult> GetStatusHistory(int id)
    {
        var history = await _bookService.GetStatusHistoryAsync(id);

        if (history is null)
        {
            return NotFound();
        }

        return Ok(history);
    }

    [HttpGet("{id:int}/notes")]
    public async Task<IActionResult> GetNotes(int id)
    {
        var notes = await _bookService.GetNotesAsync(id);

        if (notes is null)
        {
            return NotFound();
        }

        return Ok(notes);
    }

    [HttpPost("{id:int}/notes")]
    public async Task<IActionResult> AddNote(int id, CreateReadingNoteRequest request)
    {
        try
        {
            var note = await _bookService.AddNoteAsync(id, request);

            if (note is null)
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

    [HttpPatch("{id:int}/rating")]
    public async Task<IActionResult> UpdateRating(int id, UpdateBookRatingRequest request)
    {
        try
        {
            var book = await _bookService.UpdateRatingAsync(id, request);

            if (book is null)
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
