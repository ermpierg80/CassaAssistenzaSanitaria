using System;
using System.Collections.Generic;
using CassaAssistenzaSanitaria.ViewModels;
using Xamarin.Forms;

namespace CassaAssistenzaSanitaria.Views
{
    public partial class ChangePwd : ContentPage
    {
        public ChangePwd(ChangePwdModel viewModel)
        {
            InitializeComponent();
            viewModel.Navigation = Navigation;
            BindingContext = viewModel;
        }

        void Submit_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "LoginChange");
        }
    }
}
