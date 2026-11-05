using System.Net;
using Library.Domain.Entities;
using Library.Infrastructure.Repositories;

namespace Library.Tests.Repositories;

public class BookRepositoryTests
{
    private static BookRepository CreateRepository(FakeHttpHandler handler)
    {
        var client = new HttpClient(handler) { BaseAddress = new Uri("https://fakerestapi.azurewebsites.net/") };
        return new BookRepository(client);
    }

    [Fact]
    public async Task GetAllAsync_ReturnsBooksFromApi()
    {
        var handler = new FakeHttpHandler(HttpStatusCode.OK, """[{"id":1,"title":"Book 1"},{"id":2,"title":"Book 2"}]""");
        var repository = CreateRepository(handler);

        var result = (await repository.GetAllAsync()).ToList();

        Assert.Equal(2, result.Count);
        Assert.Equal("Book 1", result[0].Title);
        Assert.Equal("/api/v1/Books", handler.LastRequest!.RequestUri!.AbsolutePath);
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsNull_WhenApiReturnsNotFound()
    {
        var handler = new FakeHttpHandler(HttpStatusCode.NotFound);
        var repository = CreateRepository(handler);

        var result = await repository.GetByIdAsync(999);

        Assert.Null(result);
    }

    [Fact]
    public async Task CreateAsync_SendsPostToBooksEndpoint()
    {
        var handler = new FakeHttpHandler(HttpStatusCode.OK, """{"id":201,"title":"Nuevo"}""");
        var repository = CreateRepository(handler);

        var result = await repository.CreateAsync(new Book { Id = 201, Title = "Nuevo" });

        Assert.Equal(HttpMethod.Post, handler.LastRequest!.Method);
        Assert.Equal(201, result.Id);
    }

    [Fact]
    public async Task DeleteAsync_Throws_WhenApiFails()
    {
        var handler = new FakeHttpHandler(HttpStatusCode.InternalServerError);
        var repository = CreateRepository(handler);

        await Assert.ThrowsAsync<HttpRequestException>(() => repository.DeleteAsync(1));
    }
}
