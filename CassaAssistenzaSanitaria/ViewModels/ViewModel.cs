using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using CassaAssistenzaSanitaria.Services;
using Xamarin.Forms;

namespace CassaAssistenzaSanitaria.ViewModels
{
    public abstract class ViewModel : INotifyPropertyChanged
    {
        private string title;
        public event PropertyChangedEventHandler PropertyChanged;
        public DataStore DataStore => DependencyService.Get<DataStore>();
        
        public void RaisePropertyChanged(params string[] propertyNames)
        {
            foreach (var propertyName in propertyNames)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public INavigation Navigation { get; set; }
        public string Title
        {
            get { return title; }
            set { title = value; RaisePropertyChanged(nameof(Title)); }
        }
    }
}
