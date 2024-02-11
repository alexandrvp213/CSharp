using FOHQ.Droid.Tools;
using FOHQ.Views.Responsibility;

using Xamarin.Forms;

[assembly: Dependency(typeof(BaseUrl_Android))]
namespace FOHQ.Droid.Tools
{
    public class BaseUrl_Android : IBaseUrl
    {
        public string Get()
        {
            return "file:///android_asset/";
        }
    }
}