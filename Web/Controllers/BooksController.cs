using System.Security.Claims;
using Core;
using Core.Dto;
using Core.Entity;
using Core.Persistance;
using Core.ValueObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Web.Authorization;
using Web.Services;

namespace Web.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
[Authorize]
public class BooksController : BaseController
{
    private readonly IBookRepository _bookRepository;
    private readonly BookService _bookService;
    private readonly ILogger<BooksController> _logger;
    private readonly IUserRepository _userRepository;

    public BooksController(ILogger<BooksController> logger, IBookRepository bookRepository,
        BookService bookService)
    {
        _logger = logger;
        _bookRepository = bookRepository;
        _bookService = bookService;
    }

    [HttpGet(Name = "GetBooks")]
    [Produces<SuccessResponse<IList<Book>>>]
    public async Task<ActionResult<Ok<SuccessResponse<IList<Book>>>>> Get()
    {
        var books = await _bookRepository.GetBooksAsync();
        return TypedResults.Ok(new SuccessResponse<IList<Book>>(books));
    }

    [HttpGet("{id:guid}", Name = "GetBookById")]
    public async Task<Results<Ok<SuccessResponse<Book>>, NotFound>> GetBookById(Guid id)
    {
        var book = await _bookRepository.GetBookByIdAsync(id);
        if (book == null) return TypedResults.NotFound();

        return TypedResults.Ok(new SuccessResponse<Book>(book));
    }

    [HttpGet("search", Name = "FilterBooks")]
    [Produces<SuccessResponse<IList<Book>>>]
    public async Task<Results<Ok<IEnumerable<Book>>, ValidationProblem>> FilterBooks([FromQuery] string q)
    {
        if (string.IsNullOrWhiteSpace(q))
            return TypedResults.ValidationProblem(new Dictionary<string, string[]>()
            {
                { "q", new[] { Messages.SearchTermNotFound } }
            });

        var books = await _bookRepository.FilterBooksAsync(q);
        return TypedResults.Ok(books);
    }

    [HttpPost("", Name = "AddBook")]
    [HasPermission(PermissionType.CanCreateBooks)]
    public async Task<Created<SuccessResponse<CreateBookResponse>>> AddBook(CreateBook book)
    {
        var bookResult = await _bookService.AddBookAsync(book);
        return TypedResults.Created(nameof(AddBook),
            new SuccessResponse<CreateBookResponse>(new CreateBookResponse(bookResult.Id)));
    }

    [HttpDelete("{id:guid}", Name = "DeleteBookById")]
    [HasPermission(PermissionType.CanDeleteBooks)]
    public async Task<Results<Ok<SuccessResponse<object?>>, NotFound<FailureResponse>>> DeleteBookById(Guid id)
    {
        try
        {
            var book = await _bookRepository.DeleteBookAsync(id);
            return TypedResults.Ok(new SuccessResponse<object?>(null));
        }
        catch (KeyNotFoundException e)
        {
            return TypedResults.NotFound(new FailureResponse(Messages.NotFoundMessage));
        }
    }

    [HttpPost("borrow", Name = "BorrowBook")]
    [HasPermission(PermissionType.CanBorrowBooks)]
    public async Task<Results<Ok<SuccessResponse<object?>>, NotFound<FailureResponse>, BadRequest<FailureResponse>>>
        BorrowBook([FromBody] BorrowBook borrowBook)
    {
        var userId = User.Claims.FirstOrDefault(u => u.Type == ClaimTypes.NameIdentifier)!.Value;
        try
        {
            await _bookService.BorrowBookAsync(borrowBook, Guid.Parse(userId), borrowBook.DueDate);

            return TypedResults.Ok(new SuccessResponse<object?>(null, Messages.SuccessMessage));
        }
        catch (KeyNotFoundException e)
        {
            return TypedResults.NotFound(new FailureResponse(Messages.NotFoundMessage));
        }
        catch (DomainException e)
        {
            return TypedResults.BadRequest(new FailureResponse(e.Message));
        }
    }

    [HttpPut("{id:guid}", Name = "UpdateBook")]
    [HasPermission(PermissionType.CanUpdateBooks)]
    public async Task<Results<Ok<SuccessResponse<object?>>, NotFound<FailureResponse>, BadRequest<FailureResponse>>>
        UpdateBook(Guid id, [FromBody] UpdateBook updateBook)
    {
        var book = await _bookRepository.GetBookByIdAsync(id);
        if (book == null) return TypedResults.NotFound(new FailureResponse(Messages.NotFoundMessage));
        try
        {
            var newBookInfo = book.UpdateDetail(updateBook);
            await _bookRepository.UpdateBookAsync(newBookInfo);
            return TypedResults.Ok(new SuccessResponse<object?>(null, Messages.SuccessMessage));
        }
        catch (DomainException e)
        {
            return TypedResults.BadRequest(new FailureResponse(e.Message));
        }
    }

    [HttpPost("{bookId:guid}/return", Name = "ReturnBook")]
    [HasPermission(PermissionType.CanReturnBooks)]
    public async Task<Results<Ok<SuccessResponse<object?>>, BadRequest<FailureResponse>>> ReturnBook(Guid bookId)
    {
        try
        {
            var userId = User.Claims.FirstOrDefault(u => u.Type == ClaimTypes.NameIdentifier)?.Value;
            await _bookService.ReturnBookAsync(bookId, Guid.Parse(userId));
            return TypedResults.Ok(new SuccessResponse<object?>(null, Messages.SuccessMessage));
        }
        catch (DomainException e)
        {
            return TypedResults.BadRequest(new FailureResponse(e.Message));
        }
    }

    public record CreateBookResponse(Guid Id);
}