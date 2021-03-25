using System;
using System.Collections.Generic;
using CassaAssistenzaSanitaria.Models;
using Xamarin.Forms;
using CassaAssistenzaSanitaria.Services;

namespace CassaAssistenzaSanitaria.Views
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        async void Submit_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "LoginToken");
            Label label = Content.FindByName<Label>("Benvenuto");

            if (label != null)
            {
                if (label.Text.Contains("Benvenuto"))
                {
                    await Navigation.PushAsync(new ElencoPage());
                }
            }
        }

        void Cancel_Clicked(object sender, EventArgs e)
        {
            var closer = DependencyService.Get<ICloseApplication>();
            closer?.closeApplication();
        }
    }
}
