using Core;
using Core.Persistance;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[ApiController]
[Route("[controller]")]
public class BooksController : ControllerBase
{
    private readonly ILogger<BooksController> _logger;
    private readonly IBookRepository _bookRepository;

    public BooksController(ILogger<BooksController> logger,IBookRepository bookRepository)
    {
        _logger = logger;
        _bookRepository = bookRepository;
    }

    [HttpGet(Name = "GetBooks")]
    public async Task<IEnumerable<Book>> Get()
    {
        return await _bookRepository.GetBooksAsync();
    }
}