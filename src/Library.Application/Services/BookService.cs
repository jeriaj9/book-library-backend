using Library.Application.Interfaces;
using Library.Domain.Entities;

namespace Library.Application.Services;

public class BookService : IBookService
{
    private readonly IBookRepository _bookRepository;

    public BookService(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public Task<IEnumerable<Book>> GetAllAsync()
    {
        return _bookRepository.GetAllAsync();
    }

    public Task<Book?> GetByIdAsync(int id)
    {
        return _bookRepository.GetByIdAsync(id);
    }

    public Task<Book> CreateAsync(Book book)
    {
        return _bookRepository.CreateAsync(book);
    }

    public Task<Book> UpdateAsync(int id, Book book)
    {
        book.Id = id;
        return _bookRepository.UpdateAsync(id, book);
    }

    public Task DeleteAsync(int id)
    {
        return _bookRepository.DeleteAsync(id);
    }
}
