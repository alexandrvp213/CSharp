using System;
using System.Threading.Tasks;

using FOHQ.Views.Database;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FOHQ.Views.Main
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DatabasePage : ContentPage
    {
        public DatabasePage()
        {
            InitializeComponent();
        }

        private async void BtnRadioExchangeSides_Clicked(object sender, EventArgs e)
        {
            await OpenDatabasePage(new RadioExchangeSidesPage());
        }

        private async void BtnCombatAreasChiefs_Clicked(object sender, EventArgs e)
        {
            await OpenDatabasePage(new CombatAreasChiefsPage());
        }

        private async void BtnMainTasks_Clicked(object sender, EventArgs e)
        {
            await OpenDatabasePage(new MainTasksPage());
        }

        private async void BtnTransmittedInformation_Clicked(object sender, EventArgs e)
        {
            await OpenDatabasePage(new TransmittedInformationPage());
        }

        private async void BtnForcesAndMeans_Clicked(object sender, EventArgs e)
        {
            await OpenDatabasePage(new ForcesAndMeansPage());
        }

        private async Task OpenDatabasePage(Page page, bool isModal = false)
        {
            if (IsBusy) return;

            try
            {
                IsBusy = true;
                if (isModal)
                {
                    await Navigation.PushModalAsync(page);
                }
                else
                {
                    await Navigation.PushAsync(page);
                }
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