using System.Security.Claims;
using Core;
using Core.Dto;
using Core.Entity;
using Core.Persistance;
using Core.Services;
using Core.ValueObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Authorization;

namespace Web.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
[Authorize]
public class BooksController : BaseController
{
    private readonly BookDomainService _bookDomainService;
    private readonly IBookRepository _bookRepository;
    private readonly ILogger<BooksController> _logger;
    private readonly IUserRepository _userRepository;

    public BooksController(ILogger<BooksController> logger, IBookRepository bookRepository,
        BookDomainService bookDomainService)
    {
        _logger = logger;
        _bookRepository = bookRepository;
        _bookDomainService = bookDomainService;
    }

    [HttpGet(Name = "GetBooks")]
    [Produces<SuccessResponse<IList<Book>>>]
    public async Task<IActionResult> Get()
    {
        var books = await _bookRepository.GetBooksAsync();
        return Success(books);
    }

    [HttpGet("{id:guid}", Name = "GetBookById")]
    [Produces<SuccessResponse<Book>>]
    public async Task<IActionResult> GetBookById(Guid id)
    {
        var book = await _bookRepository.GetBookByIdAsync(id);
        if (book == null) return NotFound();

        return Success(book);
    }

    [HttpGet("search", Name = "FilterBooks")]
    [Produces<SuccessResponse<IList<Book>>>]
    public async Task<IActionResult> FilterBooks([FromQuery] string q)
    {
        if (string.IsNullOrWhiteSpace(q)) return Failure(Messages.SearchTermNotFound);

        var books = await _bookRepository.FilterBooksAsync(q);
        return Success(books);
    }

    [HttpPost("", Name = "AddBook")]
    [HasPermission(PermissionType.CanCreateBooks)]
    [ProducesResponseType(422)]
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
    [HasPermission(PermissionType.CanDeleteBooks)]
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
    [HasPermission(PermissionType.CanBorrowBooks)]
    public async Task<IActionResult> BorrowBook([FromBody] BorrowBook borrowBook)
    {
        var userId = User.Claims.FirstOrDefault(u => u.Type == ClaimTypes.NameIdentifier)!.Value;
        try
        {
            await _bookDomainService.BorrowBookAsync(borrowBook, Guid.Parse(userId));

            return Success<object>(null, Messages.SuccessMessage);
        }
        catch (KeyNotFoundException e)
        {
            return Failure(Messages.BookNotFound, 404);
        }
        catch (DomainException e)
        {
            return Failure(e.Message);
        }
    }

    [HttpPut("{id:guid}", Name = "UpdateBook")]
    [HasPermission(PermissionType.CanUpdateBooks)]
    public async Task<IActionResult> UpdateBook(Guid id, [FromBody] UpdateBook updateBook)
    {
        var book = await _bookRepository.GetBookByIdAsync(id);
        if (book == null) return Failure(Messages.BookNotFound, 404);
        try
        {
            var newBookInfo = book.UpdateDetail(updateBook);
            await _bookRepository.UpdateBookAsync(newBookInfo);
            return Success(book, Messages.SuccessMessage);
        }
        catch (DomainException e)
        {
            return Failure(e.Message);
        }
    }

    [HttpPost("{bookId:guid}/return", Name = "ReturnBook")]
    [HasPermission(PermissionType.CanReturnBooks)]
    public async Task<IActionResult> ReturnBook(Guid bookId)
    {
        try
        {
            var userId = User.Claims.FirstOrDefault(u => u.Type == ClaimTypes.NameIdentifier)?.Value;
            await _bookDomainService.ReturnBookAsync(bookId, Guid.Parse(userId));
            return Success<object>(null, Messages.SuccessMessage);
        }
        catch (DomainException e)
        {
            return Failure(e.Message);
        }
    }
}