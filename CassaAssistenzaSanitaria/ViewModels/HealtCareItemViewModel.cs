using CassaAssistenzaSanitaria.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace CassaAssistenzaSanitaria.ViewModels
{
    public class HealtCareItemViewModel : ViewModel
    {
        public HealtCareItemViewModel(HealtCareItem item) => Item = item;

        public event EventHandler ItemStatusChanged;
        public HealtCareItem Item { get; private set; }
        public string StatusText => Item.Conferma ? "Reactivate" : "Completed";

        public ICommand ToggleCompleted => new Command((arg) =>
        {
            Item.Conferma = !Item.Conferma;
            if (Item.Conferma)
            {
                Item.DataConferma = DateTime.Now;
            }
            else
            {
                Item.DataConferma = DateTime.MinValue;
            }
            ItemStatusChanged?.Invoke(this, new EventArgs());
        });

    }
}
