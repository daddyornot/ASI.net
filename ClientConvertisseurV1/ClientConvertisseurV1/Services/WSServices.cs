using ClientConvertisseurV1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Windows.Gaming.Input.ForceFeedback;
using Windows.Web.Http;

namespace ClientConvertisseurV1.Services
{
    internal class WSServices : IServices
    {
        private System.Net.Http.HttpClient client;
        private static WSServices instance = null;
        private readonly DialogService _dialogService;

        private WSServices(string uri) 
        {
            // "https://127.0.0.1:7296/api"
            client = new System.Net.Http.HttpClient();
            client.BaseAddress = new Uri(uri);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept
                .Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
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
                System.Diagnostics.Debug.WriteLine("nomControleur: " + client.BaseAddress + nomControleur );
                return await client.GetFromJsonAsync<List<Devise>>(nomControleur);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Erreur: " + ex.Message);
                System.Diagnostics.Debug.WriteLine("Stack Trace: " + ex.StackTrace);
                return null;
            }
        }
    }
}
