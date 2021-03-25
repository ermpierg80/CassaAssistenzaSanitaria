using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using CassaAssistenzaSanitaria.Models;
using CassaAssistenzaSanitaria.Views;

namespace CassaAssistenzaSanitaria.ViewModels
{
    public class LoginViewModel : BaseViewModel
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
                bool esito = await DataStore.LoginTokenAsync(login);
                if (esito)
                {
                    IscrittoAssociazione = await DataStore.GetIscrittoAsync();
                    if (IscrittoAssociazione != null)
                    {
                        Benvenuto = "Benvenuto " + IscrittoAssociazione.Nome + " " + IscrittoAssociazione.Cognome + "!";
                    }
                    else
                    {
                        Benvenuto = "Privilegi insufficienti per operare, l'utenza spesa per la login non è un iscritto al servizio!";
                    }
                }
                else
                {
                    Benvenuto = "Autenticazione fallita";
                }
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

