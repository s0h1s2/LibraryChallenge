namespace Domain.Persistance;

public interface IBookRepository
{
    Task <Book?> AddBookAsync(Book book);
}