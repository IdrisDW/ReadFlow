using Microsoft.AspNetCore.Mvc;
using ReadFlow.Application.DTOs;
using ReadFlow.Application.Services;

namespace ReadFlow.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private readonly IBookService _bookService;

    public BooksController(IBookService bookService)
    {
        _bookService = bookService;
    }

    [HttpGet]
    public ActionResult<IEnumerable<BookDto>> GetBooks()
    {
        var books = _bookService.GetAll();

        return Ok(books);
    }

    [HttpGet("{id:int}")]
    public ActionResult<BookDto> GetBookById(int id)
    {
        var book = _bookService.GetById(id);

        if (book is null)
        {
            return NotFound();
        }

        return Ok(book);
    }
}