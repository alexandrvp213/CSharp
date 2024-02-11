using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using FOHQ.Models;
using FOHQ.Data;

namespace FOHQ.Views.Main
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OpenDocumentPage : ContentPage
    {
        private readonly IList<Document> _documents = new ObservableCollection<Document>();

        public OpenDocumentPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            var documents = await App.DataAccess.DocumentRepository.GetItemsAsync();
            _documents.Clear();
            foreach (var document in documents)
            {
                _documents.Add(document);
            }
            BindingContext = _documents;
            listDocuments.ItemsSource = _documents;

            base.OnAppearing();
        }

        private async void ItemEditDocument_Clicked(object sender, EventArgs e)
        {
            if (IsBusy) return;

            var item = (MenuItem)sender;
            if (!(item.CommandParameter is Document document)) return;
            try
            {
                IsBusy = true;

                await Navigation.PushModalAsync(new Views.Main.NewDocumentPage(document));
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

        private async void ItemDeleteDocument_Clicked(object sender, System.EventArgs e)
        {
            if (IsBusy) return;

            var item = (MenuItem)sender;
            if (!(item.CommandParameter is Document document)) return;

            if (await DisplayAlert(
                "Удаление", "Удалить выбранный документ?", "Да", "Нет"))
            {
                try
                {
                    IsBusy = true;
                    await App.DataAccess.DocumentRepository.DeleteItemAsync(document);
                    _documents.Remove(document);
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
}