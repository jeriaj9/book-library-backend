using Library.Domain.Entities;

namespace Library.Application.Interfaces;

public interface IAuthorRepository
{
    Task<IEnumerable<Author>> GetAllAsync();
    Task<Author?> GetByIdAsync(int id);
    Task<Author> CreateAsync(Author author);
    Task<Author> UpdateAsync(int id, Author author);
    Task DeleteAsync(int id);
}
