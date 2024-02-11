using Foundation;
using Xamarin.Forms;

using FOHQ.iOS.Tools;

using FOHQ.Views.Responsibility;

[assembly: Dependency(typeof(BaseUrl_iOS))]
namespace FOHQ.iOS.Tools
{
    public class BaseUrl_iOS : IBaseUrl
    {
        public string Get()
        {
            return NSBundle.MainBundle.BundlePath;
        }
    }
}