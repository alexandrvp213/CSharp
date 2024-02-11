using System;
using System.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using FOHQ.Models;
using FOHQ.Data;

namespace FOHQ.Views.NewDocument
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddCombatAreaPage : ContentPage
    {
        private readonly Document _document;
        private readonly CombatArea _combatArea;
        private readonly CombatArea _source;

        public AddCombatAreaPage(Document document, CombatArea combatArea = null)
        {
            InitializeComponent();

            _document = document ?? throw new ArgumentNullException(nameof(document));
            _combatArea = new CombatArea(_document);
            if (combatArea != null)
            {
                _source = combatArea;
                _combatArea.Copy(_source);
            }
        }

        protected override async void OnAppearing()
        {
            var chiefsData = await App.DataAccess.CombatAreaChiefRepository.GetItemsAsync();
            var chiefs = chiefsData.Select(x => x.Name).ToList();
            foreach (var item in chiefs)
            {
                pickChief.Items.Add(item);
            }

            var mainTasksData = await App.DataAccess.MainTaskRepository.GetItemsAsync();
            var mainTasks = mainTasksData.Select(x => x.Name).ToList();
            foreach (var item in mainTasks)
            {
                pickMainTask.Items.Add(item);
            }

            for (int i = 1; i <= 20; i++)
            {
                pickPersonnel.Items.Add(i.ToString());
            }
            for (int i = 1; i <= 7; i++)
            {
                pickDepartments.Items.Add(i.ToString());
                pickGasUnits.Items.Add(i.ToString());
            }
            for (int i = 1; i <= 10; i++)
            {
                pickBarrelsRs50.Items.Add(i.ToString());
                pickBarrelsRs70.Items.Add(i.ToString());
                pickBarrelsVarFlow.Items.Add(i.ToString());
                pickBarrelsL.Items.Add(i.ToString());
                pickBarrelsGps.Items.Add(i.ToString());
                pickBarrelsSvp.Items.Add(i.ToString());
                pickBarrelsOther.Items.Add(i.ToString());
            }

            BindingContext = _combatArea;
            if (_source != null)
            {
                pickPersonnel.SelectedItem = _source.Personnel.ToString();
                pickDepartments.SelectedItem = _source.Departments.ToString();
                pickGasUnits.SelectedItem = _source.GasUnits.ToString();
                pickBarrelsRs50.SelectedItem = _source.BarrelsRs50.ToString();
                pickBarrelsRs70.SelectedItem = _source.BarrelsRs70.ToString();
                pickBarrelsVarFlow.SelectedItem = _source.BarrelsVarFlow.ToString();
                pickBarrelsL.SelectedItem = _source.BarrelsL.ToString();
                pickBarrelsGps.SelectedItem = _source.BarrelsGps.ToString();
                pickBarrelsSvp.SelectedItem = _source.BarrelsSvp.ToString();
                pickBarrelsOther.SelectedItem = _source.BarrelsOther.ToString();
            }

            base.OnAppearing();
        }

        private void BtnClearBarrelsRs50_OnClicked(object sender, EventArgs e)
        {
            pickBarrelsRs50.SelectedItem = "";
        }

        private void BtnClearBarrelsRs70_OnClicked(object sender, EventArgs e)
        {
            pickBarrelsRs70.SelectedItem = "";
        }

        private void BtnClearBarrelsVarFlow_OnClicked(object sender, EventArgs e)
        {
            pickBarrelsVarFlow.SelectedItem = "";
        }

        private void BtnClearBarrelsL_OnClicked(object sender, EventArgs e)
        {
            pickBarrelsL.SelectedItem = "";
        }

        private void BtnClearBarrelsGps_OnClicked(object sender, EventArgs e)
        {
            pickBarrelsGps.SelectedItem = "";
        }

        private void BtnClearBarrelsSvp_OnClicked(object sender, EventArgs e)
        {
            pickBarrelsSvp.SelectedItem = "";
        }

        private void BtnClearBarrelsOther_OnClicked(object sender, EventArgs e)
        {
            pickBarrelsOther.SelectedItem = "";
        }

        private async void BtnSave_Clicked(object sender, EventArgs e)
        {
            btnSave.IsEnabled = false;
            try
            {
                if ((pickChief.SelectedIndex < 0) ||
                    (pickMainTask.SelectedIndex < 0) ||
                    (pickCombatArea.SelectedIndex < 0) ||
                    (pickPersonnel.SelectedIndex < 0) ||
                    (pickDepartments.SelectedIndex < 0) ||
                    (pickGasUnits.SelectedIndex < 0))
                {
                    await DisplayAlert(
                        "Неполная информация",
                        "Заполните все необходимые поля.",
                        "OK");
                }
                else
                {
                    _combatArea.Personnel = GetIntValueFromPicker(pickPersonnel);
                    _combatArea.Departments = GetIntValueFromPicker(pickDepartments);
                    _combatArea.GasUnits = GetIntValueFromPicker(pickGasUnits);
                    _combatArea.BarrelsRs50 = GetIntValueFromPicker(pickBarrelsRs50);
                    _combatArea.BarrelsRs70 = GetIntValueFromPicker(pickBarrelsRs70);
                    _combatArea.BarrelsVarFlow = GetIntValueFromPicker(pickBarrelsVarFlow);
                    _combatArea.BarrelsL = GetIntValueFromPicker(pickBarrelsL);
                    _combatArea.BarrelsGps = GetIntValueFromPicker(pickBarrelsGps);
                    _combatArea.BarrelsSvp = GetIntValueFromPicker(pickBarrelsSvp);
                    _combatArea.BarrelsOther = GetIntValueFromPicker(pickBarrelsOther);

                    if (_source == null)
                    {
                        _document.CombatAreas.Add(_combatArea);
                    }
                    else
                    {
                        _source.Copy(_combatArea);
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