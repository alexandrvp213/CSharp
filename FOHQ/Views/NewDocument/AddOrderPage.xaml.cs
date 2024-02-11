using System;
using System.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using FOHQ.Models;
using FOHQ.Data;

namespace FOHQ.Views.NewDocument
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddOrdersPage : ContentPage
    {
        private readonly Document _document;
        private readonly Order _order;
        private readonly Order _source;

        public AddOrdersPage(Document document, Order order = null)
        {
            InitializeComponent();

            _document = document ?? throw new ArgumentNullException(nameof(document));
            _order = new Order(_document);
            if (order != null)
            {
                _source = order;
                _order.Copy(_source);
            }
        }

        protected override async void OnAppearing()
        {
            var informationData = await App.DataAccess.TransmittedInformationRepository.GetItemsAsync();
            var information = informationData.Select(x => x.Name).ToList();
            foreach (var item in information)
            {
                pickInformation.Items.Add(item);
            }

            var radioSidesData = await App.DataAccess.RadioExchangeSideRepository.GetItemsAsync();
            var radioSides = radioSidesData.Select(x => x.Name).ToList();
            foreach (var item in radioSides)
            {
                pickRecipient.Items.Add(item);
                pickSender.Items.Add(item);
                pickReceived.Items.Add(item);
            }

            BindingContext = _order;
            if (_source == null)
            {
                dateReceipt.Date = DateTime.Now;
                timeReceipt.Time = DateTime.Now.TimeOfDay;
            }

            base.OnAppearing();
        }

        private async void BtnSave_Clicked(object sender, EventArgs e)
        {
            btnSave.IsEnabled = false;
            try
            {
                if ((pickInformation.SelectedIndex < 0) ||
                    (pickRecipient.SelectedIndex < 0) ||
                    (pickSender.SelectedIndex < 0) ||
                    (pickReceived.SelectedIndex < 0))
                {
                    await DisplayAlert(
                        "Неполная информация",
                        "Заполните все необходимые поля.",
                        "OK");
                }
                else
                {
                    if (_source == null)
                    {
                        _document.Orders.Add(_order);
                    }
                    else
                    {
                        _source.Copy(_order);
                    }
                    await App.DataAccess.DocumentRepository.SaveItemAsync(_document);
                    await Navigation.PopModalAsync();
                }
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

        private async void BtnCancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}