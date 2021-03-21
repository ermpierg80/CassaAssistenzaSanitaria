using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Globalization;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using CassaAssistenzaSanitaria.API.Data;

namespace CassaAssistenzaSanitaria.API.Models
{
    public class GestisciADMDB
    {
        private IConfiguration Configuration { get; }
        private log4net.ILog log;

        public GestisciADMDB(IConfiguration IConfig)
        {
            log = Logger.GetLogger(typeof(GestisciADMDB));
            Configuration = IConfig;
            CultureInfo.CurrentCulture = new CultureInfo(Configuration.GetValue<string>("CurrentCulture"), false);
        }

        /*
        public string GetRichiesteFromUser(string CodiceFiscale)
        {
            try
            {
                using (CassaAssistenzaSanitaria.API.Models.CassaAssistenzaADMDbContext context = new CassaAssistenzaSanitaria.API.Models.CassaAssistenzaADMDbContext(Configuration.GetConnectionString("ADMConnection")))
                {
                    var query = from t in context.Richieste.Include("Tipologia").Include("Richiedente")
                                where t.Richiedente.CodiceFiscale.Equals(CodiceFiscale)
                                select t;

                    string output = "";

                    foreach (var t in query)
                    {
                        if (t != null)
                        {
                            output = t.ToString();
                        }
                    }
                    return output;
                }
            }
            catch (Exception e)
            {
                log.Error(e.ToString());
                return e.ToString();
            }
        }
        */

        public List<Iscritto> GetIscritti()
        {
            List<Iscritto> iscritti = null;

            try
            {
                using (CassaAssistenzaSanitaria.API.Models.CassaAssistenzaADMDbContext context = new CassaAssistenzaSanitaria.API.Models.CassaAssistenzaADMDbContext(Configuration.GetConnectionString("ADMConnection")))
                {
                    var query = from t in context.Iscritti
                                where t.DataIscrizione.Year > 1 && t.DataCancellazione.Year == 1
                                select t;

                    if (query.Count<Iscritto>() > 0)
                    {
                        iscritti = new List<Iscritto>();

                        foreach (var t in query)
                        {
                            if (t != null)
                            {
                                iscritti.Add(t);
                            }
                        }
                    }
                }
                return iscritti;
            }
            catch (Exception e)
            {
                log.Error(e.ToString());
                throw new Exception(e.Message, e.InnerException);
            }
        }

        public Iscritto GetIscritto(int id, string codiceFiscale)
        {
            Iscritto iscritto = null;

            try
            {
                using (CassaAssistenzaSanitaria.API.Models.CassaAssistenzaADMDbContext context = new CassaAssistenzaSanitaria.API.Models.CassaAssistenzaADMDbContext(Configuration.GetConnectionString("ADMConnection")))
                {
                    var query = from t in context.Iscritti
                                where t.DataIscrizione.Year > 1 && t.DataCancellazione.Year == 1 && t.Id == id && (t.CodiceFiscale == codiceFiscale || codiceFiscale == "*")
                                select t;

                    foreach (var t in query)
                    {
                        if (t != null)
                        {
                            iscritto = t;
                        }
                    }
                }
                return iscritto;
            }
            catch (Exception e)
            {
                log.Error(e.ToString());
                throw new Exception(e.Message, e.InnerException);
            }
        }

        public void AddIscritto(IscrittoModel iscritto)
        {
            try
            {
                using (CassaAssistenzaSanitaria.API.Models.CassaAssistenzaADMDbContext context = new CassaAssistenzaSanitaria.API.Models.CassaAssistenzaADMDbContext(Configuration.GetConnectionString("ADMConnection")))
                {
                    var recordIscritto = new Iscritto
                    {
                        Nome = iscritto.Nome,
                        Cognome = iscritto.Cognome,
                        CodiceFiscale = iscritto.CodiceFiscale,
                        IBAN = iscritto.IBAN,
                        DataIscrizione = DateTime.Parse(iscritto.DataIscrizione),
                        DataCancellazione = DateTime.Parse(iscritto.DataCancellazione)
                    };
                    context.Iscritti.Add(recordIscritto);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                log.Error(e.ToString());
                throw new Exception(e.Message, e.InnerException);
            }
        }

        public void UpdIscritto(int id, IscrittoModel iscritto)
        {
            try
            {
                using (CassaAssistenzaSanitaria.API.Models.CassaAssistenzaADMDbContext context = new CassaAssistenzaSanitaria.API.Models.CassaAssistenzaADMDbContext(Configuration.GetConnectionString("ADMConnection")))
                {
                    var recordIscritto = context.Iscritti.FirstOrDefault(item => item.Id.Equals(id));

                    if (recordIscritto != null)
                    {
                        recordIscritto.Nome = iscritto.Nome;
                        recordIscritto.Cognome = iscritto.Cognome;
                        recordIscritto.CodiceFiscale = iscritto.CodiceFiscale;
                        recordIscritto.IBAN = iscritto.IBAN;
                        recordIscritto.DataIscrizione = DateTime.Parse(iscritto.DataIscrizione);
                        recordIscritto.DataCancellazione = DateTime.Parse(iscritto.DataCancellazione);

                        context.SaveChanges();
                    }
                 }
            }
            catch (Exception e)
            {
                log.Error(e.ToString());
                throw new Exception(e.Message, e.InnerException);
            }
        }

        public void DelIscritto(int id)
        {
            try
            {
                using (CassaAssistenzaSanitaria.API.Models.CassaAssistenzaADMDbContext context = new CassaAssistenzaSanitaria.API.Models.CassaAssistenzaADMDbContext(Configuration.GetConnectionString("ADMConnection")))
                {
                    var recordIscritto = context.Iscritti.FirstOrDefault(item => item.Id.Equals(id));

                    if (recordIscritto != null)
                    {
                        context.Iscritti.Remove(recordIscritto);
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception e)
            {
                log.Error(e.ToString());
                throw new Exception(e.Message, e.InnerException);
            }
        }
    }
}