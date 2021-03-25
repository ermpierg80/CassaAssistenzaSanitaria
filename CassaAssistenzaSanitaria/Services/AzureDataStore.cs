using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Essentials;
using CassaAssistenzaSanitaria.Models;
using System.Net.Http.Headers;
using System.Text.Json;

namespace CassaAssistenzaSanitaria.Services
{
    public class AzureDataStore : DataStore
    {
        HttpClient client;
        IEnumerable<Richiesta> richieste;
        IEnumerable<Prestazione> prestazioni;
        Iscritto iscritto;
        string loginToken;
        bool IsConnected => Connectivity.NetworkAccess == NetworkAccess.Internet;

        public AzureDataStore()
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

        public async Task<Iscritto> GetIscrittoAsync()
        {
            Iscritto iscritto = null;
            try
            {
                if (!string.IsNullOrEmpty(loginToken))
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", loginToken);
                    var response = client.GetAsync(@"/api/Iscritti/0").Result;
                    var output_response = await response.Content.ReadAsStringAsync();

                    if (!string.IsNullOrEmpty(output_response))
                    {
                        var options = new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true,
                        };
                        iscritto = System.Text.Json.JsonSerializer.Deserialize<Iscritto>(output_response, options);
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e.InnerException);
            }
            return iscritto;
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

        public async Task<bool> LoginTokenAsync(Login login)
        {
            bool output = false;
            loginToken = null;
            if (login != null && login.Username != null && login.Password != null && IsConnected)
            {
                var serializedItem = JsonConvert.SerializeObject(login);
                try
                {
                    var response = client.PostAsync($"api/Authenticate/login", new StringContent(JsonConvert.SerializeObject(login), System.Text.Encoding.UTF8, "application/json")).Result;
                    var output_response = await response.Content.ReadAsStringAsync();

                    if (!string.IsNullOrEmpty(output_response))
                    {
                        loginToken = System.Text.Json.JsonSerializer.Deserialize<Autenticazione>(output_response).token;
                        if (!string.IsNullOrEmpty(loginToken))
                        {
                            output = true;
                        }
                    }
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message, e.InnerException);
                }
            }
            return output;
        }

        public Task<bool> UpdateRichiestaAsync(Richiesta richiesta)
        {
            throw new NotImplementedException();
        }


    }
}
