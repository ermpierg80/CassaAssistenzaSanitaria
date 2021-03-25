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

        void Submit_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "LoginToken");
        }

        void Cancel_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "RemoveToken");

            var closer = DependencyService.Get<ICloseApplication>();
            closer?.closeApplication();
        }
    }
}
