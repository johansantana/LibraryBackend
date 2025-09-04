using Domain;
using System.Net.Http.Json;

namespace Infrastructure.Repositories;

public class AuthorRepository
{
    private readonly HttpClient _http;

    public AuthorRepository(HttpClient http)
    {
        _http = http;
    }

    public async Task<IEnumerable<Author>> GetAllAsync()
    {
        var result = await _http.GetFromJsonAsync<IEnumerable<Author>>(ApiUrls.Authors).ConfigureAwait(false);
        return result ?? Enumerable.Empty<Author>();
    }

    public async Task<Author?> GetByIdAsync(int id)
        => await _http.GetFromJsonAsync<Author>($"{ApiUrls.Authors}/{id}").ConfigureAwait(false);

    public async Task<Author?> CreateAsync(Author author)
    {
        var response = await _http.PostAsJsonAsync(ApiUrls.Authors, author).ConfigureAwait(false);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<Author>().ConfigureAwait(false);
    }

    public async Task<Author?> UpdateAsync(int id, Author author)
    {
        var response = await _http.PutAsJsonAsync($"{ApiUrls.Authors}/{id}", author).ConfigureAwait(false);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<Author>().ConfigureAwait(false);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var response = await _http.DeleteAsync($"{ApiUrls.Authors}/{id}").ConfigureAwait(false);
        return response.IsSuccessStatusCode;
    }
}
