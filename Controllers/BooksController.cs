using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using books_management.Models;
using books_management.Data;
using books_management.Utils;
using Serilog;

namespace books_management.Controllers;
[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private readonly LibraryContext _context;
    private readonly ILogger<BooksController> _logger;

    public BooksController(LibraryContext context, ILogger<BooksController> logger)
    {
        _context = context;
        _logger  = logger;
    }

    // GET: api/Books/all
    [HttpGet("/api/Books/all")]
    public async Task<ActionResult<PagedResponse<Book>>> GetBooks()
    {
        var response = new PagedResponse<Book>();
        _logger.LogInformation("Fetching all books.");

        try
        {
            var books = await _context.Books.ToListAsync();

            if (books == null || !books.Any())
            {
                _logger.LogInformation("No books found.");
                response.Items        = Enumerable.Empty<Book>();
                response.TotalResults = 0;

                return NotFound(response);
            }

            _logger.LogInformation($"Successfully fetched {books.Count} books.");
            response.Items        = books;
            response.TotalResults = books.Count;

            return Ok(response);
        }
        catch (Exception ex)
        {
             var result = new Response();
            _logger.LogError(ex, "An error occurred while fetching books.");
            result.IsSuccess = false;
            result.Message   = ex.Message;

            return StatusCode(StatusCodes.Status500InternalServerError, response);
        }
    }


    // GET: api/Books/all
    [HttpGet("/api/Books/all/params")]
    public async Task<ActionResult<PagedResponse<Book>>> GetBooks([FromQuery] string title = null, [FromQuery] int? authorId = null, [FromQuery] string isbn = null, [FromQuery] DateTime? createdFrom = null, [FromQuery] DateTime? createdTo = null)
    {
        var response = new PagedResponse<Book>();
        _logger.LogInformation("Fetching books with filters: Title={Title}, AuthorId={AuthorId}, ISBN={ISBN}, CreatedFrom={CreatedFrom}, CreatedTo={CreatedTo}.", title, authorId, isbn, createdFrom, createdTo);

        try
        {
            if (createdTo.HasValue && !createdFrom.HasValue)
               return BadRequest("The 'createdFrom' parameter is required when 'createdTo' is provided.");
            
            if (createdFrom.HasValue && !createdTo.HasValue)
                createdTo = DateTime.Now; 

            var query = _context.Books.AsQueryable();

            query = query
                .Where(b => string.IsNullOrWhiteSpace(title) || b.Title.Contains(title, StringComparison.OrdinalIgnoreCase))
                .Where(b => !authorId.HasValue || b.AuthorId == authorId.Value)
                .Where(b => string.IsNullOrWhiteSpace(isbn) || b.Isbn.Contains(isbn))
                .Where(b => !createdFrom.HasValue || (b.PublishedDate >= createdFrom && (!createdTo.HasValue || b.PublishedDate <= createdTo)));

            var books = await query.ToListAsync();

            if (books == null || !books.Any())
            {
                _logger.LogInformation("No books found.");
                response.Items        = Enumerable.Empty<Book>();
                response.TotalResults = 0;

                return NotFound(response);
            }

            _logger.LogInformation($"Successfully fetched {books.Count} books.");
            response.Items        = books;
            response.TotalResults = books.Count;

            return Ok(response);
        }
        catch (Exception ex)
        {
            var result = new Response();
            _logger.LogError(ex, "An error occurred while fetching books.");
            result.IsSuccess = false;
            result.Message   = ex.Message;

            return StatusCode(StatusCodes.Status500InternalServerError, result);
        }
    }

}