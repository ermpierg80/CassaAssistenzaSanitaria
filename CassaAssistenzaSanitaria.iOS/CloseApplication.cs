using System.Threading;
using CassaAssistenzaSanitaria.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(CassaAssistenzaSanitaria.iOS.CloseApplication))]
namespace CassaAssistenzaSanitaria.iOS
{
    public class CloseApplication : ICloseApplication
    {
        public void closeApplication()
        {
            Thread.CurrentThread.Abort();
        }
    }
}
