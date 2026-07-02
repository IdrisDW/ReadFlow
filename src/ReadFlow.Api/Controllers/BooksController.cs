using Microsoft.AspNetCore.Mvc;
using ReadFlow.Api.DTOs;
using ReadFlow.Application.Interfaces;
using ReadFlow.Domain.Entities;

namespace ReadFlow.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private readonly IBookRepository _bookRepository;

    public BooksController(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    [HttpGet]
    public ActionResult<List<Book>> GetAll()
    {
        return Ok(_bookRepository.GetAll());
    }

    [HttpGet("{id}")]
    public ActionResult<Book> GetById(int id)
    {
        var book = _bookRepository.GetById(id);

        if (book is null)
        {
            return NotFound();
        }

        return Ok(book);
    }

    [HttpPost]
    public ActionResult<Book> Create(CreateBookRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Title))
        {
            return BadRequest("Title is required.");
        }

        if (string.IsNullOrWhiteSpace(request.AuthorName))
        {
            return BadRequest("AuthorName is required.");
        }

        if (string.IsNullOrWhiteSpace(request.Genre))
        {
            return BadRequest("Genre is required.");
        }

        if (!string.IsNullOrWhiteSpace(request.Isbn) &&
            _bookRepository.ExistsByIsbn(request.Isbn))
        {
            return BadRequest("A book with this ISBN already exists.");
        }


        var book = new Book
        {
            Title = request.Title,
            AuthorName = request.AuthorName,
            Genre = request.Genre,
            Isbn = request.Isbn,
            Year = request.Year
        };

        var createdBook = _bookRepository.Create(book);

        return CreatedAtAction(
            nameof(GetById),
            new { id = createdBook.Id },
            createdBook
        );
    }
}
