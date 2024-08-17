using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using books_management.Models;
using books_management.Data;
using books_management.Utils;
using Serilog;

namespace books_management.Controllers;

[ApiController]
[Route("[controller]")]

public class BooksController : ControllerBase
{
    private readonly LibraryContext _context;
    private readonly ILogger<BooksController> _logger;

    public BooksController(LibraryContext context, ILogger<BooksController> logger)
    {
        _context = context;
        _logger = logger;
    }

    // GET: api/Books/all
    [HttpGet("/api/books/all")]
    public async Task<ActionResult<PagedResponse<Book>>> GetBooks()
    {
        _logger.LogInformation("Fetching all books.");

        try
        {
            var books = await _context.Books.ToListAsync();

            if (books == null || !books.Any())
            {
                _logger.LogInformation("No books found.");
                return NotFound(new PagedResponse<Book> { Items = Enumerable.Empty<Book>(), TotalResults = 0 });
            }

            _logger.LogInformation("Successfully fetched {Count} books.", books.Count);
            return Ok(new PagedResponse<Book> { Items = books, TotalResults = books.Count });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while fetching books.");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }
}


