using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using ClientConvertisseurV2_2.Models;

namespace ClientConvertisseurV2_2.Services
{
    /// <summary>
    /// Classe permettant de gérer les services web
    /// </summary>
    internal class WSServices : IServices
    {
        private HttpClient _client;
        private static WSServices _instance = null;

        private WSServices(string uri) 
        {
            // "https://localhost:7296/api/"
            _client = new HttpClient();
            _client.BaseAddress = new Uri(uri);
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept
                .Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        /// <summary>
        /// Singleton pour l'instance de WSServices
        /// </summary>
        /// <param name="uri">L'url de l'api</param>
        /// <returns>L'instance de la classe</returns>
        public static WSServices GetInstance(string uri)
        {
            if (_instance == null)
            {
                _instance = new WSServices(uri);
            }
            return _instance;
        }

        /// <summary>
        /// Méthode permettant de récupérer les devises depuis l'API
        /// </summary>
        /// <param name="nomControleur">Le nom du endpoint</param>
        /// <returns>Une liste de devises</returns>
        public async Task<List<Devise>> GetDevisesAsync(string nomControleur)
        {
            try
            {
                return await _client.GetFromJsonAsync<List<Devise>>(nomControleur);
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
