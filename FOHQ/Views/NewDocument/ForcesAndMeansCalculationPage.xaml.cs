using System;
using System.Threading.Tasks;

using FOHQ.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using FOHQ.Models;

namespace FOHQ.Views.NewDocument
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ForcesAndMeansCalculationPage : ContentPage
    {
        private readonly Document _document;
        public ForcesAndMeansCalculationPage(Document document)
        {
            InitializeComponent();

            _document = document ?? throw new ArgumentNullException(nameof(document));
        }

        private async void BtnQuenching_Clicked(object sender, EventArgs e)
        {
            await OpenPage(new QuenchingPage(_document), true);
        }

        private async void BtnRequiredConsumption_Clicked(object sender, EventArgs e)
        {
            await OpenPage(new RequiredConsumptionPage(_document), true);
        }

        private async void BtnNumberOfDevices_Clicked(object sender, EventArgs e)
        {
            await OpenPage(new NumberOfDevicesPage(_document), true);
        }

        private async void BtnQuenchingSolution_Clicked(object sender, EventArgs e)
        {
           await OpenPage(new QuenchingSolutionPage(_document), true);
        }

        private async void BtnFilingMethods_Clicked(object sender, EventArgs e)
        {
            await OpenPage(new FilingMethodsPage(_document), true);
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

        private async void BtnSave_Clicked(object sender, EventArgs e)
        {
            btnSave.IsEnabled = false;
            try
            {
                await App.DataAccess.DocumentRepository.SaveItemAsync(_document);
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ошибка", ex.Message, "OK");
            }
            finally
            {
                btnSave.IsEnabled = true;
            }
        }

        async void Btn_GenerateDocument(object sender, EventArgs args)
        {
            btnGenerateDocument.IsEnabled = false;
            try
            {
                IsBusy = true;
                var exportWord = new ExportWord(_document);
                exportWord.Execute();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ошибка", ex.Message, "OK");
            }
            finally
            {
                btnGenerateDocument.IsEnabled = true;
                IsBusy = false;
            }
        }
    }
}

