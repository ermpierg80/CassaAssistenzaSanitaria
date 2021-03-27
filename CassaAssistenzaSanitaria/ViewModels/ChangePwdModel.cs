using Xamarin.Forms;

using CassaAssistenzaSanitaria.Models;
using CassaAssistenzaSanitaria.Views;
using CassaAssistenzaSanitaria.Repositories;

namespace CassaAssistenzaSanitaria.ViewModels
{
    public class ChangePwdModel : ViewModel
    {
        private ChangeLogin login;
        private string esito;

        public ChangePwdModel()
        {
            Title = "Cambio Password";
            
            Login = new ChangeLogin() { Username = "", OldPassword = "", NewPassword = "" };
            MessagingCenter.Subscribe<ChangePwd>(this, "LoginChange", async (obj) =>
            {
                var esito = await DataStore.LoginChangeAsync(Login);

                if (esito)
                {
                    Esito = "Credenziali variate correttamente";
                }
                else
                {
                    Esito = "Errore nella variazione della password";
                }
            });
           
        }

        public ChangeLogin Login
        {
            get { return login; }
            set { login = value; RaisePropertyChanged(nameof(Login)); }
        }

        public string Esito
        {
            get { return esito; }
            set { esito = value; RaisePropertyChanged(nameof(Esito)); }
        }
    }
}
