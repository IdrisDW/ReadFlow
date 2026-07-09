using Microsoft.AspNetCore.Mvc;
using ReadFlow.Application.Interfaces;
using ReadFlow.Application.Requests;
using ReadFlow.Api.Responses;
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

    private static ErrorResponse CreateErrorResponse(string message, int statusCode)
    {
        return new ErrorResponse
        {
            Message = message,
            StatusCode = statusCode
        };
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] BookQueryParameters queryParameters)
    {
        var result = await _bookService.GetPagedAsync(queryParameters);

        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var book = await _bookService.GetByIdAsync(id);

        if (book is null)
        {
            return NotFound(CreateErrorResponse("Book not found", 404));
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
            return BadRequest(CreateErrorResponse(ex.Message, 400));
        }
        catch (Exception)
        {
            return StatusCode(500, CreateErrorResponse("Unexpected error", 500));
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
                return NotFound(CreateErrorResponse("Book not found", 404));
            }

            return Ok(book);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(CreateErrorResponse(ex.Message, 400));
        }
        catch (Exception)
        {
            return StatusCode(500, CreateErrorResponse("Unexpected error", 500));
        }
    }

    [HttpGet("{id:int}/status-history")]
    public async Task<IActionResult> GetStatusHistory(int id)
    {
        var history = await _bookService.GetStatusHistoryAsync(id);

        if (history is null)
        {
            return NotFound(CreateErrorResponse("Book not found", 404));
        }

        return Ok(history);
    }

    [HttpGet("{id:int}/notes")]
    public async Task<IActionResult> GetNotes(int id)
    {
        var notes = await _bookService.GetNotesAsync(id);

        if (notes is null)
        {
            return NotFound(CreateErrorResponse("Book not found", 404));
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
                return NotFound(CreateErrorResponse("Book not found", 404));
            }

            return Ok(note);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(CreateErrorResponse(ex.Message, 400));
        }
        catch (Exception)
        {
            return StatusCode(500, CreateErrorResponse("Unexpected error", 500));
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
                return NotFound(CreateErrorResponse("Book not found", 404));
            }

            return Ok(book);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(CreateErrorResponse(ex.Message, 400));
        }
        catch (Exception)
        {
            return StatusCode(500, CreateErrorResponse("Unexpected error", 500));
        }
      
    }

}
