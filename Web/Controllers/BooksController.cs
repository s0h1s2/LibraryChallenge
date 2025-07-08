using Core;
using Core.Persistance;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[ApiController]
[Route("[controller]")]
public class BooksController : BaseController
{
    private readonly ILogger<BooksController> _logger;
    private readonly IBookRepository _bookRepository;

    public BooksController(ILogger<BooksController> logger,IBookRepository bookRepository)
    {
        _logger = logger;
        _bookRepository = bookRepository;
    }

    [HttpGet(Name = "GetBooks")]
    public async Task<IActionResult> Get()
    {
        var books = await _bookRepository.GetBooksAsync();
        return Success(books);
    }

    [HttpGet("{id:guid}", Name = "GetBookById")]
    public async Task<IActionResult> GetBookById(Guid id)
    {
        var book = await _bookRepository.GetBookByIdAsync(id);
        if (book == null)
        {
            return NotFound();
        }

        return Success(book,null);
    }
    [HttpDelete("{id:guid}", Name = "DeleteBookById")]
    public async Task<IActionResult> DeleteBookById(Guid id)
    {
        try
        {
            var book = await _bookRepository.DeleteBookAsync(id);
            return Success(true,Messages.SuccessMessage);
        }
        catch ( KeyNotFoundException e)
        {
            return Failure(e.Message, 404);
        }
    }
}