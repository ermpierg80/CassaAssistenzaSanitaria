using CassaAssistenzaSanitaria.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CassaAssistenzaSanitaria.Views;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using System.Linq;
using CassaAssistenzaSanitaria.Models;

namespace CassaAssistenzaSanitaria.ViewModels
{
    public class MainViewModel : ViewModel
    {
        private readonly HealtCareItemRepository repository;

        public ObservableCollection<HealtCareItemViewModel> Items { get; set; }

        public MainViewModel(HealtCareItemRepository repository)
        {
            Title = "Cassa Assistenza";
            repository.OnItemAdded += (sender, item) => Items.Add(CreateTodoItemViewModel(item));
            repository.OnItemUpdated += (sender, item) => Task.Run(async () => await LoadData());

            this.repository = repository;
            Task.Run(async () => await LoadData());
        }

        private async Task LoadData()
        {
            var items = await repository.GetItems();
            if (!ShowAll)
            {
                items = items.Where(x => x.Conferma == false).ToList();
            }

            var itemViewModels = items.Select(i => CreateTodoItemViewModel(i));
            Items = new ObservableCollection<HealtCareItemViewModel>(itemViewModels);
        }

        private async Task SincData()
        {
            var items = await repository.GetItems();
            foreach(HealtCareItem ricInLocale in items)
            {
                if (ricInLocale.Conferma)
                {
                    if (DataStore.AddRichiestaAsync(new Richiesta()
                    {
                        Id = 0,
                        IdRichiedente = ricInLocale.Richiedente,
                        IdTipologia = ricInLocale.Tipologia,
                        NumeroFattura = ricInLocale.NumeroFattura,
                        ImportoFattura = ricInLocale.ImportoFattura,
                        ImportoRimborsatoDaTerzi = ricInLocale.ImportoRimborsatoDaTerzi,
                        ImportoACarico = ricInLocale.ImportoACarico,
                        ImportoDaRimborsare = ricInLocale.ImportoDaRimborsare,
                        Note = ricInLocale.Note,
                        DataRichiesta = ricInLocale.DataRichiesta,
                        DataFattura = ricInLocale.DataFattura,
                        DataConferma = ricInLocale.DataConferma,
                        DataCancellazione = DateTime.MinValue
                    }))
                    {
                        await repository.RemoveItem(ricInLocale);
                    }
                }
                else
                {
                    if (ricInLocale.Trasmessa)
                    {
                        await repository.RemoveItem(ricInLocale);
                    }
                }
            }
            HealtCareItem newRicInLocale = null;
            IEnumerable<Richiesta> allRicInRemoto = await DataStore.GetRichiesteAsync();
            foreach(Richiesta ricInRemoto in allRicInRemoto)
            {
                newRicInLocale = new HealtCareItem() {
                    Id = 0,
                    Richiedente = ricInRemoto.IdRichiedente,
                    Tipologia = ricInRemoto.IdTipologia,
                    NumeroFattura = ricInRemoto.NumeroFattura,
                    ImportoFattura = ricInRemoto.ImportoFattura,
                    ImportoRimborsatoDaTerzi = ricInRemoto.ImportoRimborsatoDaTerzi,
                    ImportoACarico = ricInRemoto.ImportoACarico,
                    ImportoDaRimborsare = ricInRemoto.ImportoDaRimborsare,
                    Note = ricInRemoto.Note,
                    DataRichiesta = ricInRemoto.DataRichiesta,
                    DataFattura = ricInRemoto.DataFattura,
                    DataConferma = ricInRemoto.DataConferma,
                    DataTrasmissione = DateTime.Now,
                    Conferma = false,
                    Trasmessa = true
                };

                await repository.AddItem(newRicInLocale);
            }

        }

        private HealtCareItemViewModel CreateTodoItemViewModel(HealtCareItem item)
        {
            var itemViewModel = new HealtCareItemViewModel(item);
            itemViewModel.ItemStatusChanged += ItemStatusChanged;
            return itemViewModel;
        }

        private void ItemStatusChanged(object sender, EventArgs e)
        {
            if (sender is HealtCareItemViewModel item)
            {
                if (!ShowAll && item.Item.Conferma)
                {
                    Items.Remove(item);
                }

                Task.Run(async () => await repository.UpdateItem(item.Item));
            }

        }
        public bool ShowAll { get; set; }
        public bool ShowSinc { get; set; }

        public ICommand AddItem => new Command(async () =>
        {
            var itemView = new ItemView(new ItemViewModel(new HealtCareItemRepository()));
            await Navigation.PushAsync(itemView);
        });

        public HealtCareItemViewModel SelectedItem
        {
            get { return null; }
            set
            {
                Device.BeginInvokeOnMainThread(async () => await NavigateToItem(value));
                RaisePropertyChanged(nameof(SelectedItem));
            }
        }

        private async Task NavigateToItem(HealtCareItemViewModel item)
        {
            if (item == null)
            {
                return;
            }

            var itemView = new ItemView(new ItemViewModel(new HealtCareItemRepository()));
            var vm = itemView.BindingContext as ItemViewModel;
            vm.Item = item.Item;

            await Navigation.PushAsync(itemView);
        }

        public string FilterText => ShowAll ? "Tutte" : "Da Confermare";

        public ICommand ToggleFilter => new Command(async () =>
        {
            ShowAll = !ShowAll;
            await LoadData();
        });

        public ICommand Sincronizza => new Command(async () =>
        {
            ShowSinc = !ShowSinc;
            await SincData();
        });
    }
}
