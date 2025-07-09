using Core;
using Core.Dto;
using Core.Persistance;
using Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[ApiController]
[Route("[controller]")]
public class BooksController : BaseController
{
    private readonly ILogger<BooksController> _logger;
    private readonly IBookRepository _bookRepository;
    private readonly BookDomainService _bookDomainService;

    public BooksController(ILogger<BooksController> logger, IBookRepository bookRepository, BookDomainService bookDomainService)
    {
        _logger = logger;
        _bookRepository = bookRepository;
        _bookDomainService = bookDomainService;
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

        return Success(book, null);
    }

    [HttpGet("search", Name = "FilterBooks")]
    public async Task<IActionResult> FilterBooks([FromQuery] string q)
    {
        if (string.IsNullOrWhiteSpace(q))
        {
            return Failure(Messages.SearchTermNotFound);
        }

        var books = await _bookRepository.FilterBooksAsync(q);
        return Success(books);
    }

    [HttpPost("", Name = "AddBook")]
    public async Task<IActionResult> AddBook(CreateBook book)
    {
        var bookResult = await _bookDomainService.AddBookAsync(book);
        return CreatedAtAction(
            nameof(AddBook),
            new { id = bookResult?.Id },
            bookResult
        );
    }

    [HttpDelete("{id:guid}", Name = "DeleteBookById")]
    public async Task<IActionResult> DeleteBookById(Guid id)
    {
        try
        {
            var book = await _bookRepository.DeleteBookAsync(id);
            return Success(true, Messages.SuccessMessage);
        }
        catch (KeyNotFoundException e)
        {
            return Failure(e.Message, 404);
        }
    }

    [HttpPost("borrow", Name = "BorrowBook")]
    public async Task<IActionResult> BorrowBook([FromBody] BorrowBook borrowBook)
    {
        try
        {
            await _bookDomainService.BorrowBookAsync(borrowBook);
            return Success<object>(null, Messages.SuccessMessage);
        }
        catch (KeyNotFoundException e)
        {
            return Failure(Messages.BookNotFound, 404);
        }
        catch (DomainException e)
        {
            return Failure(e.Message, 400);
        }
    }

    [HttpPost("{id:guid}/return", Name = "ReturnBook")]
    public async Task<IActionResult> ReturnBook(Guid id)
    {
        var book = await _bookRepository.GetBookByIdAsync(id);
        if (book == null)
        {
            return NotFound();
        }

        try
        {
            book.Return();
            await _bookRepository.UpdateBookAsync(book);
            return Success<object>(null, Messages.SuccessMessage);
        }
        catch (DomainException e)
        {
            return Failure(e.Message, 400);
        }
    }
}