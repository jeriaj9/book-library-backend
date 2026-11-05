using Library.Application.Interfaces;
using Library.Application.Services;
using Library.Domain.Entities;
using Moq;

namespace Library.Tests.Services;

public class BookServiceTests
{
    private readonly Mock<IBookRepository> _repositoryMock = new();
    private readonly BookService _service;

    public BookServiceTests()
    {
        _service = new BookService(_repositoryMock.Object);
    }

    [Fact]
    public async Task GetAllAsync_ReturnsBooksFromRepository()
    {
        var books = new List<Book> { new() { Id = 1, Title = "Book 1" }, new() { Id = 2, Title = "Book 2" } };
        _repositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(books);

        var result = await _service.GetAllAsync();

        Assert.Equal(2, result.Count());
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsNull_WhenBookDoesNotExist()
    {
        _repositoryMock.Setup(r => r.GetByIdAsync(99)).ReturnsAsync((Book?)null);

        var result = await _service.GetByIdAsync(99);

        Assert.Null(result);
    }

    [Fact]
    public async Task UpdateAsync_UsesIdFromRoute()
    {
        var book = new Book { Id = 5, Title = "Editado" };
        _repositoryMock.Setup(r => r.UpdateAsync(10, It.IsAny<Book>())).ReturnsAsync((int _, Book b) => b);

        var result = await _service.UpdateAsync(10, book);

        Assert.Equal(10, result.Id);
    }

    [Fact]
    public async Task DeleteAsync_CallsRepository()
    {
        await _service.DeleteAsync(3);

        _repositoryMock.Verify(r => r.DeleteAsync(3), Times.Once);
    }
}
