using Library.Application.Interfaces;
using Library.Application.Services;
using Library.Domain.Entities;
using Moq;

namespace Library.Tests.Services;

public class AuthorServiceTests
{
    private readonly Mock<IAuthorRepository> _repositoryMock = new();
    private readonly AuthorService _service;

    public AuthorServiceTests()
    {
        _service = new AuthorService(_repositoryMock.Object);
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsAuthor()
    {
        var author = new Author { Id = 1, IdBook = 1, FirstName = "Juan", LastName = "Perez" };
        _repositoryMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(author);

        var result = await _service.GetByIdAsync(1);

        Assert.NotNull(result);
        Assert.Equal("Juan", result.FirstName);
    }

    [Fact]
    public async Task CreateAsync_ReturnsCreatedAuthor()
    {
        var author = new Author { Id = 600, IdBook = 2, FirstName = "Ana", LastName = "Gomez" };
        _repositoryMock.Setup(r => r.CreateAsync(author)).ReturnsAsync(author);

        var result = await _service.CreateAsync(author);

        Assert.Equal(600, result.Id);
    }

    [Fact]
    public async Task UpdateAsync_UsesIdFromRoute()
    {
        var author = new Author { Id = 1, FirstName = "Ana" };
        _repositoryMock.Setup(r => r.UpdateAsync(7, It.IsAny<Author>())).ReturnsAsync((int _, Author a) => a);

        var result = await _service.UpdateAsync(7, author);

        Assert.Equal(7, result.Id);
    }
}
