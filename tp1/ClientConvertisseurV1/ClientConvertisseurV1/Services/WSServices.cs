using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using ClientConvertisseurV1.Models;

namespace ClientConvertisseurV1.Services
{
    internal class WSServices : IServices
    {
        private HttpClient client;
        private static WSServices instance = null;
        private readonly DialogService _dialogService;

        private WSServices(string uri) 
        {
            // "https://127.0.0.1:7296/api"
            client = new HttpClient();
            client.BaseAddress = new Uri(uri);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept
                .Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public static WSServices GetInstance(string uri)
        {
            if (instance == null)
            {
                instance = new WSServices(uri);
            }
            return instance;
        }

        public async Task<List<Devise>> GetDevisesAsync(string nomControleur)
        {
            try
            {
                // ex. nomControleur : "devises"
                Debug.WriteLine("nomControleur: " + client.BaseAddress + nomControleur );
                return await client.GetFromJsonAsync<List<Devise>>(nomControleur);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Erreur: " + ex.Message);
                Debug.WriteLine("Stack Trace: " + ex.StackTrace);
                return null;
            }
        }
    }
}
