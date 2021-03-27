using CassaAssistenzaSanitaria.Models;
using CassaAssistenzaSanitaria.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CassaAssistenzaSanitaria.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ItemView : ContentPage
	{
		public ItemView (ItemViewModel viewmodel)
		{
			InitializeComponent();
            viewmodel.Navigation = Navigation;
            BindingContext = viewmodel;

            MessagingCenter.Subscribe<ItemViewModel>(this, "MostraAvviso", async (obj) =>
            {
                await DisplayAlert("Attenzione", "Non è possible modificare una richiesta trasmessa!", "OK");
            });
        }

        void Picker_BindingContextChanged(System.Object sender, System.EventArgs e)
        {
            var picker = (Picker)sender;
            if (picker.SelectedItem != null)
            {
                MessagingCenter.Send(this, "TipologiaPrestazioneSelezionata", ((Prestazione)picker.SelectedItem).Id);
            }
        }

        void Importo_TextChanged(System.Object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            var entry = (Entry)sender;
            entry.Text = entry.Text.Replace(",", ".");
        }

        void Switch_PropertyChanged(System.Object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            var switchConfermata = (Switch)sender;

            if (switchConfermata != null)
            {
                MessagingCenter.Send(this, "ChangeConfermata", switchConfermata.IsToggled);
            }
        }
    }
} 