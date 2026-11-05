using Library.Application.Interfaces;
using Library.Domain.Entities;

namespace Library.Application.Services;

public class AuthorService : IAuthorService
{
    private readonly IAuthorRepository _authorRepository;

    public AuthorService(IAuthorRepository authorRepository)
    {
        _authorRepository = authorRepository;
    }

    public Task<IEnumerable<Author>> GetAllAsync()
    {
        return _authorRepository.GetAllAsync();
    }

    public Task<Author?> GetByIdAsync(int id)
    {
        return _authorRepository.GetByIdAsync(id);
    }

    public Task<Author> CreateAsync(Author author)
    {
        return _authorRepository.CreateAsync(author);
    }

    public Task<Author> UpdateAsync(int id, Author author)
    {
        author.Id = id;
        return _authorRepository.UpdateAsync(id, author);
    }

    public Task DeleteAsync(int id)
    {
        return _authorRepository.DeleteAsync(id);
    }
}
