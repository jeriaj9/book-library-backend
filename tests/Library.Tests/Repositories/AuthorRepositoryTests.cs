using System.Net;
using Library.Infrastructure.Repositories;

namespace Library.Tests.Repositories;

public class AuthorRepositoryTests
{
    private static AuthorRepository CreateRepository(FakeHttpHandler handler)
    {
        var client = new HttpClient(handler) { BaseAddress = new Uri("https://fakerestapi.azurewebsites.net/") };
        return new AuthorRepository(client);
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsAuthorFromApi()
    {
        var handler = new FakeHttpHandler(HttpStatusCode.OK, """{"id":3,"idBook":1,"firstName":"First Name 3","lastName":"Last Name 3"}""");
        var repository = CreateRepository(handler);

        var result = await repository.GetByIdAsync(3);

        Assert.NotNull(result);
        Assert.Equal(1, result.IdBook);
        Assert.Equal("/api/v1/Authors/3", handler.LastRequest!.RequestUri!.AbsolutePath);
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsNull_WhenApiReturnsNotFound()
    {
        var handler = new FakeHttpHandler(HttpStatusCode.NotFound);
        var repository = CreateRepository(handler);

        var result = await repository.GetByIdAsync(999);

        Assert.Null(result);
    }
}
