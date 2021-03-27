using System;
using System.Collections.Generic;
using CassaAssistenzaSanitaria.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CassaAssistenzaSanitaria.Services;
using CassaAssistenzaSanitaria.ViewModels;
using CassaAssistenzaSanitaria.Repositories;

namespace CassaAssistenzaSanitaria.Views
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();

            MessagingCenter.Subscribe<LoginViewModel>(this, "ChangePage", async (obj) =>
            {
                await Navigation.PushAsync(new ChangePwd(new ChangePwdModel()));
            });
        }

        async void Submit_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "LoginToken");
            Label label = Content.FindByName<Label>("Benvenuto");

            if (label != null)
            {
                if (label.Text.Contains("Benvenuto"))
                {
                    await Navigation.PushAsync(new MainView(new MainViewModel(new HealtCareItemRepository())));
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
