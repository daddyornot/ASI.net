using System.Net.Http.Headers;
using System.Net.Http.Json;
using BlazorApp1.Models;

namespace BlazorApp1.Services;

public class WSServiceUtilisateur : IService<Utilisateur>
{
    private readonly HttpClient _httpClient;

    public WSServiceUtilisateur()
    {
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri("http://localhost:5074/api/");
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

    public async Task<Utilisateur?> GetByIdAsync(string? nomControleur, int? id)
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<Utilisateur>(nomControleur + "/" + id);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }

    public async Task<Utilisateur?> GetByStringAsync(string? nomControleur, string? str)
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<Utilisateur>(nomControleur + "/" + str);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }

    public async Task<bool> PostAsync(string? nomControleur, Utilisateur? entity)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync(nomControleur, entity);
            return response.IsSuccessStatusCode;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }

    public async Task<bool> PutAsync(string? nomControleur, int? id, Utilisateur? entity)
    {
        if (id == null)
        {
            throw new ArgumentNullException(nameof(id));
        }

        try
        {
            var response = await _httpClient.PutAsJsonAsync(nomControleur + "/" + id, entity);
            return response.IsSuccessStatusCode;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }

    public Task<bool> DeleteAsync(string? nomControleur, int? id)
    {
        throw new NotImplementedException();
    }
}