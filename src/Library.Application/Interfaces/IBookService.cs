using Library.Domain.Entities;

namespace Library.Application.Interfaces;

public interface IBookService
{
    Task<IEnumerable<Book>> GetAllAsync();
    Task<Book?> GetByIdAsync(int id);
    Task<Book> CreateAsync(Book book);
    Task<Book> UpdateAsync(int id, Book book);
    Task DeleteAsync(int id);
}
