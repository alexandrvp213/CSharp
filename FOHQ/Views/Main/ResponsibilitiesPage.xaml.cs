using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

using FOHQ.Views.Responsibility;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FOHQ.Views.Main
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ResponsibilitiesPage : ContentPage
    {
        public ObservableCollection<string> Items { get; set; }

        public ResponsibilitiesPage()
        {
            InitializeComponent();
        }

        private async void BtnCommon_Clicked(object sender, EventArgs e)
        {
            await OpenResponsibilityPage("4_1_1.html", "Общие");
        }

        private async void BtnStaffChief_Clicked(object sender, EventArgs e)
        {
            await OpenResponsibilityPage("4_1_2.html", "Начальник штаба");
        }

        private async void BtnStaffDeputyChief_Clicked(object sender, EventArgs e)
        {
            await OpenResponsibilityPage("4_1_3.html", "Заместитель НШ");
        }

        private async void BtnLogisticsChief_Clicked(object sender, EventArgs e)
        {
            await OpenResponsibilityPage("4_1_4.html", "Начальник тыла");
        }

        private async void BtnCheckpointHead_Clicked(object sender, EventArgs e)
        {
            await OpenResponsibilityPage("4_1_5.html", "Начальник КПП ГДЗС");
        }

        private async void BtnLaborProtectionResponsible_Clicked(object sender, EventArgs e)
        {
            await OpenResponsibilityPage("4_1_6.html", "Ответственный по ОТ");
        }

        private async Task OpenResponsibilityPage(string pagePath, string pageTitle)
        {
            if (IsBusy) return;

            try
            {
                IsBusy = true;
                await Navigation.PushAsync(new ResponsibilityPage(pagePath, pageTitle));
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ошибка", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }

        }
    }
}