using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using FOHQ.Models;
using FOHQ.Data;

namespace FOHQ.Views.Database
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddEditForcesAndMeansPage : ContentPage
    {
        private readonly string[] _defaultEquipment =
        {
            "АА",
            "АЦ",
            "АЦП",
            "АНР",
            "АБР",
            "АПТ",
            "АПП",
            "АШ",
            "АР",
            "ПНС",
            "ПКС",
            "УКС",
            "АГ",
            "АД",
            "АО",
            "АСМ",
            "АСА",
            "АПС",
            "АЛ-30",
            "АЛ-42",
            "АЛ-60",
            "АЛ-50",
            "АЛ-52",
            "АКП-32",
            "АКП-50",
            "ППП-32",
            "МКВПСС «КИРАСИР»",
            "МПС VFR1200X",
            "Пожарный поезд",
            "ПК «Вьюн»",
            "КС-110-39",
            "МПСК Лидер-12 ПМ"
        };
        private const int QuantityMax = 10;
        private const int PersonnelMax = 20;

        private readonly ForcesAndMeansRepository _repository;
        private readonly ForcesAndMeans _item;

        public AddEditForcesAndMeansPage(ForcesAndMeansRepository repository, ForcesAndMeans item)
        {
            InitializeComponent();

            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _item = item ?? throw new ArgumentNullException(nameof(item));
        }

        protected override void OnAppearing()
        {
            foreach (var equipment in _defaultEquipment)
            {
                pickEquipment.Items.Add(equipment);
            }
            for (int i = 1; i <= QuantityMax; i++)
            {
                pickQuantity.Items.Add(i.ToString());
            }
            for (int i = 1; i <= PersonnelMax; i++)
            {
                pickPersonnel.Items.Add(i.ToString());
            }

            if (pickEquipment.Items.IndexOf(_item.Equipment) >= 0)
            {
                pickEquipment.SelectedItem = _item.Equipment;
            }
            else
            {
                txtEquipment.Text = _item.Equipment;
            }
            pickQuantity.SelectedItem = _item.Quantity.ToString();
            pickPersonnel.SelectedItem = _item.Personnel.ToString();

            base.OnAppearing();
        }

        private async void BtnSave_Clicked(object sender, EventArgs e)
        {
            btnSave.IsEnabled = false;
            try
            {

                if (((pickEquipment.SelectedIndex < 0) && string.IsNullOrWhiteSpace(txtEquipment.Text)) ||
                     (pickQuantity.SelectedIndex < 0) ||
                     (pickPersonnel.SelectedIndex < 0))
                {
                    await DisplayAlert(
                        "Неполная информация",
                        "Заполните все необходимые поля.",
                        "OK");
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(txtEquipment.Text))
                    {
                        _item.Equipment = txtEquipment.Text;
                    }
                    else
                    {
                        _item.Equipment = pickEquipment.Items[pickEquipment.SelectedIndex];
                    }
                    _item.Quantity = GetIntValueFromPicker(pickQuantity);
                    _item.Personnel = GetIntValueFromPicker(pickPersonnel);

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