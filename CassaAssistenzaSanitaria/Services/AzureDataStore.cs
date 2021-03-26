using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Essentials;
using CassaAssistenzaSanitaria.Models;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace CassaAssistenzaSanitaria.Services
{
    public class AzureDataStore : DataStore
    {
        HttpClient client;
        Richiesta richiesta;
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
            richiesta = null;
        }

        public bool AddRichiestaAsync(Richiesta richiesta)
        {
            bool esito = false;
            try
            {
                if (!string.IsNullOrEmpty(loginToken))
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", loginToken);
                    RichiestaSend richiestaJson = new RichiestaSend()
                    {
                        Id = richiesta.Id.ToString(),
                        IdRichiedente = richiesta.IdRichiedente.ToString(),
                        IdTipologia = richiesta.IdTipologia.ToString(),
                        NumeroFattura = richiesta.NumeroFattura,
                        Note = richiesta.Note,
                        ImportoACarico = richiesta.ImportoACarico.ToString(),
                        ImportoDaRimborsare = richiesta.ImportoDaRimborsare.ToString(),
                        ImportoFattura = richiesta.ImportoFattura.ToString(),
                        ImportoRimborsatoDaTerzi = richiesta.ImportoRimborsatoDaTerzi.ToString(),
                        DataCancellazione = richiesta.DataCancellazione,
                        DataConferma = richiesta.DataConferma,
                        DataFattura = richiesta.DataFattura,
                        DataRichiesta = richiesta.DataRichiesta
                    };
                    var response = client.PostAsync(@"/api/Richieste", new StringContent(JsonConvert.SerializeObject(richiestaJson), System.Text.Encoding.UTF8, "application/json")).Result;
                    if(response.IsSuccessStatusCode)
                    {
                        esito = true;
                    }   
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e.InnerException);
            }
            return esito;
        }

        public async Task<Iscritto> GetIscrittoAsync(bool ForceRefresh = false)
        {
            try
            {
                if (ForceRefresh)
                {
                    if (!string.IsNullOrEmpty(loginToken))
                    {
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", loginToken);
                        var response = client.GetAsync(@"/api/Iscritti/0").Result;
                        var output_response = await response.Content.ReadAsStringAsync();

                        if (!string.IsNullOrEmpty(output_response))
                        {
                            iscritto = JsonConvert.DeserializeObject<Iscritto>(output_response);
                        }
                    }
                }
                
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e.InnerException);
            }
            return iscritto;
        }

        public async Task<IEnumerable<Prestazione>> GetPrestazioniAsync()
        {
            try
            {
                if (!string.IsNullOrEmpty(loginToken))
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", loginToken);
                    var response = client.GetAsync(@"/api/Prestazioni").Result;
                    var output_response = await response.Content.ReadAsStringAsync();

                    if (!string.IsNullOrEmpty(output_response))
                    {
                        prestazioni = JsonConvert.DeserializeObject<IEnumerable<Prestazione>>(output_response);
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e.InnerException);
            }
            return prestazioni;
        }

        public async Task<Richiesta> GetRichiestaAsync(int id)
        {
            richiesta = null;

            try
            {
                if (!string.IsNullOrEmpty(loginToken))
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", loginToken);
                    var response = client.GetAsync(@"/api/Richieste/" + id.ToString()).Result;
                    var output_response = await response.Content.ReadAsStringAsync();

                    if (!string.IsNullOrEmpty(output_response))
                    {
                        richiesta = JsonConvert.DeserializeObject<Richiesta>(output_response);
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e.InnerException);
            }

            return richiesta;
        }

        public async Task<IEnumerable<Richiesta>> GetRichiesteAsync()
        {
            try
            {
                if (!string.IsNullOrEmpty(loginToken))
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", loginToken);
                    var response = client.GetAsync(@"/api/Richieste").Result;
                    var output_response = await response.Content.ReadAsStringAsync();

                    if (!string.IsNullOrEmpty(output_response))
                    {
                        richieste = JsonConvert.DeserializeObject<IEnumerable<Richiesta>>(output_response);
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e.InnerException);
            }
            return richieste;
        }

        public async Task<bool> LoginTokenAsync(Login login)
        {
            bool output = false;
            loginToken = null;
            if (login != null && login.Username != null && login.Password != null && IsConnected)
            {
                try
                {
                    var response = client.PostAsync($"api/Authenticate/login", new StringContent(JsonConvert.SerializeObject(login), System.Text.Encoding.UTF8, "application/json")).Result;
                    var output_response = await response.Content.ReadAsStringAsync();

                    if (!string.IsNullOrEmpty(output_response))
                    {
                        Autenticazione Aut = JsonConvert.DeserializeObject<Autenticazione>(output_response);
                        loginToken = Aut.token;
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

        public bool UpdateRichiestaAsync(Richiesta richiesta)
        {
            bool esito = false;
            try
            {
                if (!string.IsNullOrEmpty(loginToken))
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", loginToken);
                    var response = client.PutAsync(@"/api/Richieste", new StringContent(JsonConvert.SerializeObject(richiesta), System.Text.Encoding.UTF8, "application/json")).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        esito = true;
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e.InnerException);
            }
            return esito;
        }
    }
}
