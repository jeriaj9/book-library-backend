using System.Net;
using System.Net.Http.Json;
using Library.Application.Interfaces;
using Library.Domain.Entities;

namespace Library.Infrastructure.Repositories;

public class AuthorRepository : IAuthorRepository
{
    private const string Endpoint = "api/v1/Authors";
    private readonly HttpClient _httpClient;

    public AuthorRepository(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<Author>> GetAllAsync()
    {
        var authors = await _httpClient.GetFromJsonAsync<IEnumerable<Author>>(Endpoint);
        return authors ?? [];
    }

    public async Task<Author?> GetByIdAsync(int id)
    {
        var response = await _httpClient.GetAsync($"{Endpoint}/{id}");

        if (response.StatusCode == HttpStatusCode.NotFound)
            return null;

        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<Author>();
    }

    public async Task<Author> CreateAsync(Author author)
    {
        var response = await _httpClient.PostAsJsonAsync(Endpoint, author);
        response.EnsureSuccessStatusCode();
        return (await response.Content.ReadFromJsonAsync<Author>())!;
    }

    public async Task<Author> UpdateAsync(int id, Author author)
    {
        var response = await _httpClient.PutAsJsonAsync($"{Endpoint}/{id}", author);
        response.EnsureSuccessStatusCode();
        return (await response.Content.ReadFromJsonAsync<Author>())!;
    }

    public async Task DeleteAsync(int id)
    {
        var response = await _httpClient.DeleteAsync($"{Endpoint}/{id}");
        response.EnsureSuccessStatusCode();
    }
}
