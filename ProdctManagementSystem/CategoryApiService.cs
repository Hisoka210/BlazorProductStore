using System.Net.Http.Json;
using BlazorProductStore.DAL.Models;

public class CategoryApiService
{
    private readonly HttpClient _httpClient;

    public CategoryApiService(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("WebAPI");
    }

    public async Task<List<Category>?> GetCategoriesAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<Category>>("api/categories");
    }
}