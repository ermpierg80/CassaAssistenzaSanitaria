using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Essentials;
using CassaAssistenzaSanitaria.Models;
using System.Net.Http.Headers;
using System.Net;

namespace CassaAssistenzaSanitaria.Services
{
    public class AzureCADataStore : CADataStore
    {
        HttpClient client;
        IEnumerable<Richiesta> richieste;
        IEnumerable<Prestazione> prestazioni;
        Iscritto iscritto;
        string loginToken;
        bool IsConnected => Connectivity.NetworkAccess == NetworkAccess.Internet;

        public AzureCADataStore()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri($"{App.AzureBackendUrl}/");
            //System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            //client.DefaultRequestHeaders.Accept.Clear();
            //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            richieste = new List<Richiesta>();
            prestazioni = new List<Prestazione>();
        }

        public Task<bool> AddRichiestaAsync(Richiesta richiesta)
        {
            throw new NotImplementedException();
        }

        public Task<Iscritto> GetIscrittoAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Prestazione>> GetPrestazioniAsync(bool forceRefresh = false)
        {
            throw new NotImplementedException();
        }

        public Task<Richiesta> GetRichiestaAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Richiesta>> GetRichiesteAsync(bool forceRefresh = false)
        {
            throw new NotImplementedException();
        }

        public async Task<string> LoginTokenAsync(Login login)
        {
            if (login != null && login.Username != null && login.Password != null && IsConnected)
            {
                var serializedItem = JsonConvert.SerializeObject(login);
                try
                {
                    var response = client.PostAsync($"api/Authenticate/login", new StringContent(JsonConvert.SerializeObject(login), System.Text.Encoding.UTF8, "application/json")).Result;
                    loginToken = await response.Content.ReadAsStringAsync();
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message, e.InnerException);
                }
                return loginToken;
            }
            return null;
        }

        public Task<bool> UpdateRichiestaAsync(Richiesta richiesta)
        {
            throw new NotImplementedException();
        }
    }
}
