using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using FOHQ.Models;

namespace FOHQ.Views.Database
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainTasksPage : ContentPage
    {
        private readonly IList<MainTask> _items = new ObservableCollection<MainTask>();

        public MainTasksPage()
        {
            BindingContext = _items;
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            var items = await App.DataAccess.MainTaskRepository.GetItemsAsync();
            _items.Clear();
            foreach (var item in items)
            {
                _items.Add(item);
            }

            base.OnAppearing();
        }

        private async void BtnAddItem_Clicked(object sender, EventArgs e)
        {
            if (IsBusy) return;

            try
            {
                IsBusy = true;
                await Navigation.PushModalAsync(
                    new AddEditDbDirectoryObjectPage<MainTask>(
                        App.DataAccess.MainTaskRepository, new MainTask()));
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

        private async void MenuEditItem_Clicked(object sender, EventArgs e)
        {
            if (IsBusy) return;

            var menuItem = (MenuItem)sender;
            if (!(menuItem.CommandParameter is MainTask item)) return;

            try
            {
                IsBusy = true;
                await Navigation.PushModalAsync(
                    new AddEditDbDirectoryObjectPage<MainTask>(
                        App.DataAccess.MainTaskRepository, item));
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

        private async void MenuDeleteItem_Clicked(object sender, EventArgs e)
        {
            if (IsBusy) return;

            var menuItem = (MenuItem)sender;
            if (!(menuItem.CommandParameter is MainTask item)) return;

            if (await DisplayAlert(
                "Удаление", "Удалить выбранный элемент?", "Да", "Нет"))
            {
                try
                {
                    IsBusy = true;
                    await App.DataAccess.MainTaskRepository.DeleteItemAsync(item);
                    _items.Remove(item);
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