using System.Net.Http.Headers;
using System.Net.Http.Json;
using BlazorApp1.Models;

namespace BlazorApp1.Services;

public class WSServiceUtilisateur: IService<Utilisateur>
{
    private readonly HttpClient _httpClient;

    public WSServiceUtilisateur()
    {
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri("http://localhost:5074/");
        _httpClient.DefaultRequestHeaders.Accept.Clear();
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }

    public async Task<List<Utilisateur>?> GetAllAsync(string? nomControleur)
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<List<Utilisateur>>(nomControleur);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }

    public Task<Utilisateur?> GetByIdAsync(string? nomControleur, int? id)
    {
        throw new NotImplementedException();
    }

    public Task<Utilisateur?> GetByStringAsync(string? nomControleur, string? str)
    {
        throw new NotImplementedException();
    }

    public Task<bool> PostAsync(string? nomControleur, Utilisateur? entity)
    {
        try
        {
            var response = _httpClient.PostAsJsonAsync(nomControleur, entity).Result;
            return Task.FromResult(response.IsSuccessStatusCode);

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Task.FromResult(false);
        }
    }

    public Task<bool> PutAsync(string? nomControleur, int? id, Utilisateur? entity)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(string? nomControleur, int? id)
    {
        throw new NotImplementedException();
    }
}