using System;
using System.Collections.Generic;
using CassaAssistenzaSanitaria.Models;
using Xamarin.Forms;

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
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "RemoveToken");
        }
    }
}
