using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using FOHQ.Data;
using FOHQ.Models;

namespace FOHQ.Views.Database
{
    /// <summary>
    /// Добавление данных в справочник.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public class AddEditDbDirectoryObjectPage<T> : ContentPage where T : DbDirectoryObject, new()
    {
        private readonly EntryCell _txtName;
        private readonly Button _btnSave;

        private readonly DbDirectoryObjectRepository<T> _repository;
        private readonly T _item;

        public AddEditDbDirectoryObjectPage(DbDirectoryObjectRepository<T> repository, T item)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _item = item ?? throw new ArgumentNullException(nameof(item));

            var tableView = new TableView
            {
                Intent = TableIntent.Form,
                Root = new TableRoot() {
                    new TableSection("Наименование") {
                        (_txtName = new EntryCell {
                            Label = "Наименование",
                            Text = _item.Name,
                        })
                    }
                }
            };

            var btnCancel = new Button()
            {
                BackgroundColor = Color.DarkRed,
                TextColor = Color.White,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Margin = 8,
                Text = "Отмена"
            };
            btnCancel.Clicked += BtnCancel_Clicked;

            _btnSave = new Button()
            {
                BackgroundColor = Color.Green,
                TextColor = Color.White,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Margin = 8,
                Text = "Сохранить"
            };
            _btnSave.Clicked += BtnSave_Clicked;

            var stackLayout = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Children = { btnCancel, _btnSave }
            };

            Content = new StackLayout
            {
                Children = { tableView, stackLayout }
            };
        }

        protected override void OnAppearing()
        {
            _txtName.Text = _item.Name;

            base.OnAppearing();
        }

        private async void BtnSave_Clicked(object sender, EventArgs e)
        {
            _btnSave.IsEnabled = false;
            try
            {
                if (string.IsNullOrWhiteSpace(_txtName.Text))
                {
                    await DisplayAlert(
                        "Неполная информация",
                        "Заполните поле Наименование.",
                        "OK");
                }
                else
                {
                    _item.Name = _txtName.Text;
                    await _repository.SaveItemAsync(_item);
                    await Navigation.PopModalAsync();
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ошибка", ex.Message, "OK");
            }
            finally
            {
                _btnSave.IsEnabled = true;
            }
        }

        private async void BtnCancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}