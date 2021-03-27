using System;
using Android.App;
using CassaAssistenzaSanitaria.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(CassaAssistenzaSanitaria.Droid.CloseApplication))]
namespace CassaAssistenzaSanitaria.Droid
{
    public class CloseApplication : ICloseApplication
    {
        public void closeApplication()
        {
            (Android.App.Application.Context as Activity).Finish();
        }
    }
}
