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
                                where (t.Id == id || id == 0) && (t.CodiceFiscale == codiceFiscale || codiceFiscale == "*")
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

        public List<Prestazione> GetPrestazioni()
        {
            List<Prestazione> prestazioni = null;

            try
            {
                using (CassaAssistenzaSanitaria.API.Models.CassaAssistenzaADMDbContext context = new CassaAssistenzaSanitaria.API.Models.CassaAssistenzaADMDbContext(Configuration.GetConnectionString("ADMConnection")))
                {
                    var query = from t in context.Prestazioni
                                where t.DataCreazione.Year > 1 && t.DataCancellazione.Year == 1
                                select t;

                    if (query.Count<Prestazione>() > 0)
                    {
                        prestazioni = new List<Prestazione>();

                        foreach (var t in query)
                        {
                            if (t != null)
                            {
                                prestazioni.Add(t);
                            }
                        }
                    }
                }
                return prestazioni;
            }
            catch (Exception e)
            {
                log.Error(e.ToString());
                throw new Exception(e.Message, e.InnerException);
            }
        }

        public Prestazione GetPrestazione(int id)
        {
            Prestazione prestazione = null;
            try
            {
                using (CassaAssistenzaSanitaria.API.Models.CassaAssistenzaADMDbContext context = new CassaAssistenzaSanitaria.API.Models.CassaAssistenzaADMDbContext(Configuration.GetConnectionString("ADMConnection")))
                {
                    var query = context.Prestazioni.FirstOrDefault<Prestazione>(item => item.Id == id);

                    if (query != null)
                    {
                        prestazione = query;
                    }
                }
                return prestazione;
            }
            catch (Exception e)
            {
                log.Error(e.ToString());
                throw new Exception(e.Message, e.InnerException);
            }
        }

        public void AddPrestazione(PrestazioneModel prestazione)
        {
            try
            {
                using (CassaAssistenzaSanitaria.API.Models.CassaAssistenzaADMDbContext context = new CassaAssistenzaSanitaria.API.Models.CassaAssistenzaADMDbContext(Configuration.GetConnectionString("ADMConnection")))
                {
                    var recordPrestazione = new Prestazione
                    {
                        Descrizione = prestazione.Descrizione,
                        PercentualeRimborso = Decimal.Parse(prestazione.PercentualeRimborso),
                        DataCreazione = DateTime.Parse(prestazione.DataCrezione),
                        DataCancellazione = DateTime.Parse(prestazione.DataCancellazione)
                    };
                    context.Prestazioni.Add(recordPrestazione);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                log.Error(e.ToString());
                throw new Exception(e.Message, e.InnerException);
            }
        }

        public void UpdPrestazione(int id, PrestazioneModel prestazione)
        {
            try
            {
                using (CassaAssistenzaSanitaria.API.Models.CassaAssistenzaADMDbContext context = new CassaAssistenzaSanitaria.API.Models.CassaAssistenzaADMDbContext(Configuration.GetConnectionString("ADMConnection")))
                {
                    var recordPrestazione = context.Prestazioni.FirstOrDefault(item => item.Id.Equals(id));

                    if (recordPrestazione != null)
                    {
                        recordPrestazione.Descrizione = prestazione.Descrizione;
                        recordPrestazione.PercentualeRimborso = Decimal.Parse(prestazione.PercentualeRimborso);
                        recordPrestazione.DataCreazione = DateTime.Parse(prestazione.DataCrezione);
                        recordPrestazione.DataCancellazione = DateTime.Parse(prestazione.DataCancellazione);

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

        public void DelPrestazione(int id)
        {
            try
            {
                using (CassaAssistenzaSanitaria.API.Models.CassaAssistenzaADMDbContext context = new CassaAssistenzaSanitaria.API.Models.CassaAssistenzaADMDbContext(Configuration.GetConnectionString("ADMConnection")))
                {
                    var recordPrestazione = context.Prestazioni.FirstOrDefault(item => item.Id.Equals(id));

                    if (recordPrestazione != null)
                    {
                        context.Prestazioni.Remove(recordPrestazione);
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

        public List<Richiesta> GetRichieste(string codiceFiscale)
        {
            List<Richiesta> richieste = null;

            try
            {
                using (CassaAssistenzaSanitaria.API.Models.CassaAssistenzaADMDbContext context = new CassaAssistenzaSanitaria.API.Models.CassaAssistenzaADMDbContext(Configuration.GetConnectionString("ADMConnection")))
                {
                    var query = from t in context.Richieste
                                where t.DataRichiesta.Year > 1 && t.DataCancellazione.Year == 1 && (t.Richiedente.CodiceFiscale == codiceFiscale || codiceFiscale == "*")
                                select t;

                    if (query.Count<Richiesta>() > 0)
                    {
                        richieste = new List<Richiesta>();

                        foreach (var t in query)
                        {
                            if (t != null)
                            {
                                richieste.Add(t);
                            }
                        }
                    }
                }
                return richieste;
            }
            catch (Exception e)
            {
                log.Error(e.ToString());
                throw new Exception(e.Message, e.InnerException);
            }
        }

        public Richiesta GetRichiesta(int id, string codiceFiscale)
        {
            Richiesta richiesta = null;
            try
            {
                using (CassaAssistenzaSanitaria.API.Models.CassaAssistenzaADMDbContext context = new CassaAssistenzaSanitaria.API.Models.CassaAssistenzaADMDbContext(Configuration.GetConnectionString("ADMConnection")))
                {
                    var query = context.Richieste.FirstOrDefault<Richiesta>(item => item.Id == id && (item.Richiedente.CodiceFiscale == codiceFiscale || codiceFiscale == "*"));

                    if (query != null)
                    {
                        richiesta = query;
                    }
                }
                return richiesta;
            }
            catch (Exception e)
            {
                log.Error(e.ToString());
                throw new Exception(e.Message, e.InnerException);
            }
        }

        public void AddRichiesta(RichiestaModel richiesta, string codiceFiscale)
        {
            try
            {
                using (CassaAssistenzaSanitaria.API.Models.CassaAssistenzaADMDbContext context = new CassaAssistenzaSanitaria.API.Models.CassaAssistenzaADMDbContext(Configuration.GetConnectionString("ADMConnection")))
                {
                    var recordRichiedente = context.Iscritti.FirstOrDefault<Iscritto>(item => item.Id.Equals(int.Parse(richiesta.IdRichiedente)) && (item.CodiceFiscale == codiceFiscale || codiceFiscale == "*"));

                    if (recordRichiedente != null)
                    {
                        var recordRichiesta = new Richiesta
                        {
                            Tipologia = context.Prestazioni.FirstOrDefault<Prestazione>(item => item.Id.Equals(int.Parse(richiesta.IdTipologia))),
                            Richiedente = recordRichiedente,
                            ImportoFattura = Decimal.Parse(richiesta.ImportoFattura),
                            ImportoRimborsatoDaTerzi = Decimal.Parse(richiesta.ImportoRimborsatoDaTerzi),
                            ImportoACarico = Decimal.Parse(richiesta.ImportoACarico),
                            ImportoDaRimborsare = Decimal.Parse(richiesta.ImportoDaRimborsare),
                            NumeroFattura = richiesta.NumeroFattura,
                            Note = richiesta.Note,
                            DataFattura = DateTime.Parse(richiesta.DataFattura),
                            DataRichiesta = DateTime.Parse(richiesta.DataRichiesta),
                            DataConferma = DateTime.Parse(richiesta.DataConferma),
                            DataCancellazione = DateTime.Parse(richiesta.DataCancellazione)
                        };
                        context.Richieste.Add(recordRichiesta);
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

        public void UpdRichieste(int id, RichiestaModel richiesta, string codiceFiscale)
        {
            try
            {
                using (CassaAssistenzaSanitaria.API.Models.CassaAssistenzaADMDbContext context = new CassaAssistenzaSanitaria.API.Models.CassaAssistenzaADMDbContext(Configuration.GetConnectionString("ADMConnection")))
                {
                    var recordRichiedente = context.Iscritti.FirstOrDefault<Iscritto>(item => item.Id.Equals(int.Parse(richiesta.IdRichiedente)) && (item.CodiceFiscale == codiceFiscale || codiceFiscale == "*"));
                    var recordRichiesta = context.Richieste.FirstOrDefault<Richiesta>(item => item.Id.Equals(id) && (item.Richiedente.CodiceFiscale == codiceFiscale || codiceFiscale == "*"));

                    if (recordRichiedente != null && recordRichiesta != null)
                    {
                        recordRichiesta.Tipologia = context.Prestazioni.FirstOrDefault<Prestazione>(item => item.Id.Equals(int.Parse(richiesta.IdTipologia)));
                        recordRichiesta.Richiedente = recordRichiedente;
                        recordRichiesta.ImportoFattura = Decimal.Parse(richiesta.ImportoFattura);
                        recordRichiesta.ImportoRimborsatoDaTerzi = Decimal.Parse(richiesta.ImportoRimborsatoDaTerzi);
                        recordRichiesta.ImportoACarico = Decimal.Parse(richiesta.ImportoACarico);
                        recordRichiesta.ImportoDaRimborsare = Decimal.Parse(richiesta.ImportoDaRimborsare);
                        recordRichiesta.NumeroFattura = richiesta.NumeroFattura;
                        recordRichiesta.Note = richiesta.Note;
                        recordRichiesta.DataFattura = DateTime.Parse(richiesta.DataFattura);
                        recordRichiesta.DataRichiesta = DateTime.Parse(richiesta.DataRichiesta);
                        recordRichiesta.DataConferma = DateTime.Parse(richiesta.DataConferma);
                        recordRichiesta.DataCancellazione = DateTime.Parse(richiesta.DataCancellazione);

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

        public void DelRichieste(int id)
        {
            try
            {
                using (CassaAssistenzaSanitaria.API.Models.CassaAssistenzaADMDbContext context = new CassaAssistenzaSanitaria.API.Models.CassaAssistenzaADMDbContext(Configuration.GetConnectionString("ADMConnection")))
                {
                    var recordRichieste = context.Richieste.FirstOrDefault(item => item.Id.Equals(id));

                    if (recordRichieste != null)
                    {
                        context.Richieste.Remove(recordRichieste);
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