using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using FOHQ.Models;
using FOHQ.Data;

namespace FOHQ.Views.NewDocument
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DocumentHeaderPage : ContentPage
    {
        private readonly Document _document;

        public DocumentHeaderPage(Document document)
        {
            InitializeComponent();

            _document = document ?? throw new ArgumentNullException(nameof(document));
        }

        protected override void OnAppearing()
        {
            BindingContext = _document;
            if (_document.Id == 0)
            {
                dateDocument.Date = DateTime.Now;
                timeDocument.Time = DateTime.Now.TimeOfDay;
            }

            base.OnAppearing();
        }

        private async void BtnSaveDocument_Clicked(object sender, EventArgs e)
        {
            if (IsBusy) return;

            btnSaveDocument.IsEnabled = false;
            try
            {
                IsBusy = true;
                await App.DataAccess.DocumentRepository.SaveItemAsync(_document);
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ошибка", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
                btnSaveDocument.IsEnabled = true;
            }
        }

        private async void BtnCloseDocument_Clicked(object sender, EventArgs e)
        {
            if (await this.DisplayAlert(
                "Закрытие", "Закрыть текущий документ?", "Да", "Нет") == true)
            {
                await Navigation.PopModalAsync();
            }
        }
    }
}