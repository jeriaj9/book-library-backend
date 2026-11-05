using System.Net;
using System.Net.Http.Json;
using Library.Application.Interfaces;
using Library.Domain.Entities;

namespace Library.Infrastructure.Repositories;

public class BookRepository : IBookRepository
{
    private const string Endpoint = "api/v1/Books";
    private readonly HttpClient _httpClient;

    public BookRepository(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<Book>> GetAllAsync()
    {
        var books = await _httpClient.GetFromJsonAsync<IEnumerable<Book>>(Endpoint);
        return books ?? [];
    }

    public async Task<Book?> GetByIdAsync(int id)
    {
        var response = await _httpClient.GetAsync($"{Endpoint}/{id}");

        if (response.StatusCode == HttpStatusCode.NotFound)
            return null;

        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<Book>();
    }

    public async Task<Book> CreateAsync(Book book)
    {
        var response = await _httpClient.PostAsJsonAsync(Endpoint, book);
        response.EnsureSuccessStatusCode();
        return (await response.Content.ReadFromJsonAsync<Book>())!;
    }

    public async Task<Book> UpdateAsync(int id, Book book)
    {
        var response = await _httpClient.PutAsJsonAsync($"{Endpoint}/{id}", book);
        response.EnsureSuccessStatusCode();
        return (await response.Content.ReadFromJsonAsync<Book>())!;
    }

    public async Task DeleteAsync(int id)
    {
        var response = await _httpClient.DeleteAsync($"{Endpoint}/{id}");
        response.EnsureSuccessStatusCode();
    }
}
