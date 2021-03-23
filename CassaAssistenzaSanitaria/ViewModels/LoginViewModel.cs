using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using CassaAssistenzaSanitaria.Models;
using CassaAssistenzaSanitaria.Views;

namespace CassaAssistenzaSanitaria.ViewModels
{
    public class LoginViewModel : CABaseViewModel
    {
        private string loginToken;
        private Login login;

        public LoginViewModel()
        {
            Title = "Login Cassa Assistenza Sanitaria";
            login = new Login
            {
                Username = "ErmPierg",
                Password = "Test@123"
            };
            LoginToken = "";
            MessagingCenter.Subscribe<LoginPage>(this, "LoginToken", async (obj) =>
            {
                LoginToken = await DataStore.LoginTokenAsync(login);
            });
            MessagingCenter.Subscribe<LoginPage>(this, "RemoveToken", async (obj) =>
            {
                LoginToken = "";
            });
        }

        public string LoginToken
        {
            get { return loginToken; }
            set { SetProperty(ref loginToken, value); }
        }

        public Login Login
        {
            get { return login; }
            set { SetProperty(ref login, value); }
        }

    }
}

