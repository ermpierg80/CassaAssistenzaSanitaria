using CassaAssistenzaSanitaria.Models;
using CassaAssistenzaSanitaria.Repositories;
using CassaAssistenzaSanitaria.Services;
using CassaAssistenzaSanitaria.Views;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace CassaAssistenzaSanitaria.ViewModels
{
    public class ItemViewModel : ViewModel
    {
        private HealtCareItemRepository repository;

        public HealtCareItem Item { get; set; }
        public List<Prestazione> Prestazioni { get; set; }

        public ItemViewModel(HealtCareItemRepository repository)
        {
            this.repository = repository;
            Title = "Richiesta Rimborso";
            Iscritto IscrittoAssociazione = DataStore.GetIscrittoAsync().Result;
            IEnumerable<Prestazione> ListaPrestazioni = DataStore.GetPrestazioniAsync().Result;
            List<Prestazione> List = new List<Prestazione>();
            foreach (Prestazione p in ListaPrestazioni)
            {
                List.Add(p);
            }
            Prestazioni = List;
            RaisePropertyChanged(nameof(Prestazioni));

            Item = new HealtCareItem() { Richiedente = IscrittoAssociazione.Id, DataFattura = DateTime.Now, DataRichiesta = DateTime.Now, Conferma = false, Trasmessa = false };

            MessagingCenter.Subscribe<ItemView, int>(this, "TipologiaPrestazioneSelezionata", (obj, IdTipologia) =>
            {
                Item.Tipologia = IdTipologia;
                if (Item.ImportoFattura > 0)
                {
                    Item.ImportoACarico = Item.ImportoFattura - Item.ImportoRimborsatoDaTerzi;
                    foreach (Prestazione p in Prestazioni)
                    {
                        if (p.Id.Equals(Item.Tipologia))
                        {
                            Item.ImportoDaRimborsare = (Item.ImportoACarico * p.PercentualeRimborso) / 100;
                            break;
                        }
                    }
                }
                RaisePropertyChanged(nameof(Item));
            });

            MessagingCenter.Subscribe<ItemView, bool>(this, "ChangeConfermata", (obj, Toggled) =>
            {
                if (Toggled)
                {
                    Item.DataConferma = DateTime.Now;
                }
                else
                {
                    Item.DataConferma = DateTime.MinValue;
                }

            });
        }

        public ICommand Save => new Command(async () =>
        {
            if (!Item.Trasmessa)
            {
                await repository.AddOrUpdate(Item);
                await Navigation.PopAsync();
            }
            else
            {
                MessagingCenter.Send(this, "MostraAvviso");
            }
        });

        public ICommand Remove => new Command(async () =>
        {
            if (!Item.Trasmessa)
            {
                await repository.RemoveItem(Item);
                await Navigation.PopAsync();
            }
            else
            {
                MessagingCenter.Send(this, "MostraAvviso");
            }
        });
    }
}
