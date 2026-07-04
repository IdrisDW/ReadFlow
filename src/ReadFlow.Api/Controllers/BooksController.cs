using Microsoft.AspNetCore.Mvc;
using ReadFlow.Api.Requests;
using ReadFlow.Application.Interfaces;

namespace ReadFlow.Api.Controllers;

[ApiController]
[Route("api/books")]
public class BooksController : ControllerBase
{
    private readonly IBookRepository _bookRepository;
    private readonly IReadingStatusTransitionValidator _statusTransitionValidator;

    public BooksController(
        IBookRepository bookRepository,
        IReadingStatusTransitionValidator statusTransitionValidator)
    {
        _bookRepository = bookRepository;
        _statusTransitionValidator = statusTransitionValidator;
    }

    [HttpPatch("{id}/status")]
    public IActionResult UpdateStatus(int id, UpdateBookStatusRequest request)
    {
        var book = _bookRepository.GetById(id);

        if (book is null)
        {
            return NotFound();
        }

        var isTransitionAllowed = _statusTransitionValidator.CanTransition(
            book.Status,
            request.NewStatus
        );

        if (!isTransitionAllowed)
        {
            return BadRequest($"Cannot change status from {book.Status} to {request.NewStatus}.");
        }

        book.Status = request.NewStatus;

        var updatedBook = _bookRepository.Update(book);

        return Ok(updatedBook);
    }
}