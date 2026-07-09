using Microsoft.AspNetCore.Mvc;
using ReadFlow.Application.Interfaces;

namespace ReadFlow.Api.Controllers;

[ApiController]
[Route("api/dashboard")]
public class DashboardController : ControllerBase
{
    private readonly IBookService _bookService;

    public DashboardController(IBookService bookService)
    {
        _bookService = bookService;
    }

    [HttpGet("reading-summary")]
    public async Task<IActionResult> GetReadingSummary()
    {
        var summary = await _bookService.GetReadingSummaryAsync();

        return Ok(summary);
    }
}