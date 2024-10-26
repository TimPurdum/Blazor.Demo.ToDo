using System.Net.Http.Json;

namespace ICC2024_1.Client;

public class ClientRepository(HttpClient httpClient) : IRepository
{
    public async Task<int> AddAsync<T>(T entity) where T: class, IIdentity
    {
        HttpResponseMessage response = await httpClient.PostAsJsonAsync("api/ToDoItems", entity);
        response.EnsureSuccessStatusCode();
        return int.Parse(await response.Content.ReadAsStringAsync());
    }

    public async Task DeleteAsync<T>(int id) where T: class, IIdentity
    {
        await httpClient.DeleteAsync($"api/ToDoItems/{id}");
    }

    public async Task<List<T>> GetAllAsync<T>() where T: class, IIdentity
    {
        return (await httpClient.GetFromJsonAsync<List<T>>("api/ToDoItems"))!;
    }

    public async Task<T> GetAsync<T>(int id) where T: class, IIdentity
    {
        return (await httpClient.GetFromJsonAsync<T>($"api/ToDoItems/{id}"))!;
    }

    public async Task UpdateAsync<T>(T entity) where T: class, IIdentity
    {
        await httpClient.PutAsJsonAsync($"api/ToDoItems/{entity.Id}", entity);
    }
}
