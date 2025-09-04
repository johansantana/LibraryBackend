using Domain;
using System.Net.Http.Json;

namespace Infrastructure.Repositories;

public class BookRepository
{
    private readonly HttpClient _http;

    public BookRepository(HttpClient http)
    {
        _http = http;
    }

    public async Task<IEnumerable<Book>> GetAllAsync()
    {
        var result = await _http.GetFromJsonAsync<IEnumerable<Book>>(ApiUrls.Books).ConfigureAwait(false);
        return result ?? Enumerable.Empty<Book>();
    }

    public async Task<Book?> GetByIdAsync(int id)
        => await _http.GetFromJsonAsync<Book>($"{ApiUrls.Books}/{id}").ConfigureAwait(false);

    public async Task<Book?> CreateAsync(Book book)
    {
        var response = await _http.PostAsJsonAsync(ApiUrls.Books, book).ConfigureAwait(false);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<Book>().ConfigureAwait(false);
    }

    public async Task<Book?> UpdateAsync(int id, Book book)
    {
        var response = await _http.PutAsJsonAsync($"{ApiUrls.Books}/{id}", book).ConfigureAwait(false);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<Book>().ConfigureAwait(false);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var response = await _http.DeleteAsync($"{ApiUrls.Books}/{id}").ConfigureAwait(false);
        return response.IsSuccessStatusCode;
    }
}
