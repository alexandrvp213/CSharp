using System;
using System.Threading.Tasks;

using Xamarin.Forms;

using FOHQ.Views.Main;

namespace FOHQ
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void BtnNewDocument_Clicked(object sender, EventArgs e)
        {
            await OpenPage(new NewDocumentPage(), true);
        }

        private async void BtnOpenDocument_Clicked(object sender, EventArgs e)
        {
            await OpenPage(new OpenDocumentPage());
        }

        private async void BtnReadHandbook_Clicked(object sender, EventArgs e)
        {
            await OpenPage(new ResponsibilitiesPage());
        }

        private async void BtnDatabase_Clicked(object sender, EventArgs e)
        {
            await OpenPage(new DatabasePage());
        }

        private async Task OpenPage(Page page, bool isModal = false)
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