using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using CassaAssistenzaSanitaria.Models;
using CassaAssistenzaSanitaria.Views;

namespace CassaAssistenzaSanitaria.ViewModels
{
    public class LoginViewModel : ViewModel
    {
        private Login login;
        private Iscritto iscritto;
        private string benvenuto;

        public LoginViewModel()
        {
            Title = "Login";
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
                    IscrittoAssociazione = await DataStore.GetIscrittoAsync(true);
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
            set { iscritto = value; RaisePropertyChanged(nameof(IscrittoAssociazione)); }
        }

        public Login Login
        {
            get { return login; }
            set { login = value; RaisePropertyChanged(nameof(Login)); }
        }

        public string Benvenuto
        {
            get { return benvenuto; }
            set { benvenuto = value; RaisePropertyChanged(nameof(Benvenuto)); }
        }

    }
}

