using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using CassaAssistenzaSanitaria.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CassaAssistenzaSanitaria.Views
{
    public partial class ElencoPage : ContentPage
    {
        public ElencoPage()
        {
            InitializeComponent();

            MessagingCenter.Send(this, "ElencoRichieste");
        }

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;

            string info = string.Format("Id Richiesta: {0}\nData Richiesta:{1}\nData Fattura:{2}", ((Elenco)e.Item).id, ((Elenco)e.Item).prestazione, ((Elenco)e.Item).fattura);
            await DisplayAlert("Richiesta selezionata", info, "OK");

            //Deselect Item
            ((ListView)sender).SelectedItem = null;
        }
    }
}
