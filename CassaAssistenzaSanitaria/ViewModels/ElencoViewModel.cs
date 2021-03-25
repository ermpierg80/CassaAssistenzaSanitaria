using System.Collections.Generic;
using System.Collections.ObjectModel;
using CassaAssistenzaSanitaria.Models;
using CassaAssistenzaSanitaria.Views;
using Xamarin.Forms;

namespace CassaAssistenzaSanitaria.ViewModels
{
    public class ElencoViewModel : BaseViewModel
    {
        private IEnumerable<Richiesta> richieste;
        private IEnumerable<Prestazione> prestazioni;
        private List<Elenco> mostrarichieste = new List<Elenco>();

        public ElencoViewModel()
        {
            Title = "Elenco Richieste";

            MessagingCenter.Subscribe<ElencoPage>(this, "ElencoRichieste", async (obj) =>
            {
                richieste = await DataStore.GetRichiesteAsync();
                prestazioni = await DataStore.GetPrestazioniAsync();

                List<Elenco> r = new List<Elenco>();
                foreach (Richiesta ric in richieste)
                {
                    r.Add(new Elenco()
                    {
                        id = ric.Id,
                        fattura = string.Format("({1}/{2}/{3}) N.Fattura:{0}", ric.NumeroFattura, ric.DataFattura.Day, ric.DataFattura.Month, ric.DataFattura.Year),
                        prestazione = string.Format("({1}/{2}/{3}) Tipo:{0}", searchFromPrestazioni(ric.IdTipologia).Descrizione, ric.DataRichiesta.Day, ric.DataRichiesta.Month, ric.DataRichiesta.Year)
                    }); 
                }

                Mostrarichieste = r;
            });
        }

        public Prestazione searchFromPrestazioni(int id)
        {
            Prestazione output = null;

            foreach(Prestazione p in prestazioni)
            {
                if (p.Id.Equals(id))
                {
                    output = p;
                    break;
                }
            }

            return output;
        }

        public List<Elenco> Mostrarichieste
        {
            get { return mostrarichieste; }
            set { SetProperty(ref mostrarichieste, value); }
        }
    }
}