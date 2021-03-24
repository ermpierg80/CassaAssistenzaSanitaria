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
        private Login login;
        private Iscritto iscritto;
        private string benvenuto;

        public LoginViewModel()
        {
            Title = "Login Cassa Assistenza Sanitaria";
            login = new Login
            {
                Username = "ErmPierg",
                Password = "Test@123"
            };
            Benvenuto = "";
            IscrittoAssociazione = null;
            MessagingCenter.Subscribe<LoginPage>(this, "LoginToken", async (obj) =>
            {
                var LoginToken = await DataStore.LoginTokenAsync(login);
                if (!String.IsNullOrEmpty(LoginToken))
                {
                    IscrittoAssociazione = await DataStore.GetIscrittoAsync();
                    if (IscrittoAssociazione != null)
                    {
                        Benvenuto = "Benvenuto " + IscrittoAssociazione.Nome + " " + IscrittoAssociazione.Cognome + "!";
                    }
                    else
                    {
                        Benvenuto = "Benvenuto sconosciuto";
                    }
                }
                else
                {
                    Benvenuto = "Autenticazione fallita";
                }

            });
            MessagingCenter.Subscribe<LoginPage>(this, "RemoveToken", async (obj) =>
            {
                IscrittoAssociazione = null;
                Benvenuto = "";
            });
        }

        public Iscritto IscrittoAssociazione
        {
            get { return iscritto; }
            set { SetProperty(ref iscritto, value); }
        }

        public Login Login
        {
            get { return login; }
            set { SetProperty(ref login, value); }
        }

        public string Benvenuto
        {
            get { return benvenuto; }
            set { SetProperty(ref benvenuto, value); }
        }

    }
}

