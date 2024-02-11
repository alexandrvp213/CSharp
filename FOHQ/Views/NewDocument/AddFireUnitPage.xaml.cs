using System;
using System.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using FOHQ.Models;
using FOHQ.Data;

namespace FOHQ.Views.NewDocument
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddFireUnitPage : ContentPage
    {
        private readonly Document _document;
        private readonly FireUnit _fireUnit;
        private readonly FireUnit _source;

        public AddFireUnitPage(Document document, FireUnit fireUnit = null)
        {
            InitializeComponent();

            _document = document ?? throw new ArgumentNullException(nameof(document));
            _fireUnit = new FireUnit(_document);
            if (fireUnit != null)
            {
                _source = fireUnit;
                _fireUnit.Copy(_source);
            }
        }

        protected override async void OnAppearing()
        {
            for (int i = 1; i <= 20; i++)
            {
                pickPersonnel.Items.Add(i.ToString());
            }

            var mainTasksData = await App.DataAccess.MainTaskRepository.GetItemsAsync();
            var mainTasks = mainTasksData.Select(x => x.Name).ToList();
            foreach (var item in mainTasks)
            {
                pickMainTask.Items.Add(item);
            }

            BindingContext = _fireUnit;
            if (_source == null)
            {
                dateArrival.Date = DateTime.Now;
                timeArrival.Time = DateTime.Now.TimeOfDay;
                dateMainTask.Date = DateTime.Now;
                dateFirstBarrel.Date = DateTime.Now;
                dateDeparture.Date = DateTime.Now;
            }
            else
            {
                pickPersonnel.SelectedItem = _source.Personnel.ToString();
            }

            base.OnAppearing();
        }

        private void BtnSetMainTaskTime_Clicked(object sender, EventArgs e)
        {
            dateMainTask.Date = DateTime.Now;
            timeMainTask.Time = DateTime.Now.TimeOfDay;
        }

        private void BtnSetFirstBarrelTime_OnClicked(object sender, EventArgs e)
        {
            dateFirstBarrel.Date = DateTime.Now;
            timeFirstBarrel.Time = DateTime.Now.TimeOfDay;
        }

        private void BtnSetDepartureTime_Clicked(object sender, EventArgs e)
        {
            dateDeparture.Date = DateTime.Now;
            timeDeparture.Time = DateTime.Now.TimeOfDay;
        }

        private async void BtnSave_Clicked(object sender, EventArgs e)
        {
            btnSave.IsEnabled = false;
            try
            {
                if (string.IsNullOrWhiteSpace(txtDepartments.Text) ||
                   (pickPersonnel.SelectedIndex < 0) ||
                   (pickMainTask.SelectedIndex < 0) ||
                   (pickCombatArea.SelectedIndex < 0))
                {
                    await DisplayAlert(
                        "Неполная информация",
                        "Заполните все необходимые поля.",
                        "OK");
                }
                else
                {
                    _fireUnit.Personnel = GetIntValueFromPicker(pickPersonnel);

                    if (_source == null)
                    {
                        _document.FireUnits.Add(_fireUnit);
                    }
                    else
                    {
                        _source.Copy(_fireUnit);
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

        private int GetIntValueFromPicker(Picker picker, int @default = 0)
        {
            return picker.SelectedIndex >= 0
                ? int.TryParse(picker.Items[picker.SelectedIndex], out var result) ? result : @default
                : @default;
        }
    }
}