using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FOHQ.Views.Responsibility
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ResponsibilityPage : ContentPage
    {
        public ResponsibilityPage(string pagePath, string pageTitle)
        {
            InitializeComponent();

            Title = pageTitle.ToUpper();

            var urlSource = new UrlWebViewSource();
            urlSource.Url = System.IO.Path.Combine(DependencyService.Get<IBaseUrl>().Get(), pagePath);
            webView.Source = urlSource;
        }
    }

    public interface IBaseUrl
    {
        public string Get();
    }
}