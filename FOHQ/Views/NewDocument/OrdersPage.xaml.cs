using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using FOHQ.Models;
using FOHQ.Data;

namespace FOHQ.Views.NewDocument
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OrdersPage : ContentPage
    {
        private readonly Document _document;
        private readonly IList<Order> _orders = new ObservableCollection<Order>();

        public OrdersPage(Document document)
        {
            InitializeComponent();

            _document = document ?? throw new ArgumentNullException(nameof(document));
        }

        protected override void OnAppearing()
        {
            _orders.Clear();
            foreach (var order in _document.Orders)
            {
                _orders.Add(order);
            }
            listOrders.ItemsSource = _orders;

            base.OnAppearing();
        }

        private async void BtnAddInformation_Clicked(object sender, EventArgs e)
        {
            if (IsBusy) return;

            try
            {
                IsBusy = true;
                await App.DataAccess.DocumentRepository.SaveItemAsync(_document);
                await Navigation.PushModalAsync(new AddOrdersPage(_document));
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

        private async void ItemEditInformation_Clicked(object sender, EventArgs e)
        {
            if (IsBusy) return;

            var item = (MenuItem)sender;
            if (!(item.CommandParameter is Order order)) return;

            try
            {
                IsBusy = true;
                await Navigation.PushModalAsync(new AddOrdersPage(_document, order));
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

        private async void ItemDeleteInformation_Clicked(object sender, EventArgs e)
        {
            if (IsBusy) return;

            var item = (MenuItem)sender;
            if (!(item.CommandParameter is Order order)) return;

            if (await DisplayAlert(
                "Удаление", "Удалить выбранную запись?", "Да", "Нет"))
            {
                try
                {
                    IsBusy = true;

                    _document.Orders.Remove(order);
                    await App.DataAccess.DocumentRepository.SaveItemAsync(_document);
                    _orders.Remove(order);
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