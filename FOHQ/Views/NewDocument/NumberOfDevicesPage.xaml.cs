using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using FOHQ.Models;
using System.Threading.Tasks;

namespace FOHQ.Views.NewDocument
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NumberOfDevicesPage : ContentPage
    {
        private readonly Document _document;
        public NumberOfDevicesPage(Document document)
        {
            InitializeComponent();
            _document = document ?? throw new ArgumentNullException(nameof(document));
        }

        private void EnabledContorls()
        {
            pickWaterBarrels.IsEnabled = rbWater.IsChecked;
            entryConsumption1.IsEnabled = rbWater.IsChecked;
            btnClearWaterBarrel.IsEnabled = rbWater.IsChecked;

            entryVolume.IsEnabled = rbBulkQuenching.IsChecked;

            rbSurfaceQuenching.IsEnabled = rbFoam.IsChecked || rbSurfaceQuenching.IsChecked || rbBulkQuenching.IsChecked || rbLVJ.IsChecked || rbGJ.IsChecked;
            rbBulkQuenching.IsEnabled = rbFoam.IsChecked || rbSurfaceQuenching.IsChecked || rbBulkQuenching.IsChecked;

            rbLVJ.IsEnabled = rbSurfaceQuenching.IsChecked || rbLVJ.IsChecked || rbGJ.IsChecked;
            rbGJ.IsEnabled = rbLVJ.IsEnabled;

            entryExtinguishingAreaFoamBarrel.IsEnabled = rbLVJ.IsChecked || rbGJ.IsChecked || rbSurfaceQuenching.IsChecked || rbBulkQuenching.IsChecked;
            pickFoamBarrels.IsEnabled = entryExtinguishingAreaFoamBarrel.IsEnabled;
            btnClearFoamBarrel.IsEnabled = entryExtinguishingAreaFoamBarrel.IsEnabled;

            panelAltSolution1.IsEnabled = rbAltSolution1.IsChecked;
            panelAltSolution2.IsEnabled = rbAltSolution2.IsChecked;
        }

        private void Init()
        {
            foreach (var item in App.DataAccess.NumberOfDevicesRepository.WaterBarrelList)
            {
                pickWaterBarrels.Items.Add(item.Device);
                pickExtinguishingBarrel1.Items.Add(item.Device);
                pickExtinguishingBarrel2.Items.Add(item.Device);
                pickDefenseBarrel1.Items.Add(item.Device);
                pickDefenseBarrel2.Items.Add(item.Device);
            }

            foreach (var item in App.DataAccess.NumberOfDevicesRepository.FoamBarrelList)
            {
                pickFoamBarrels.Items.Add(item.Device);
                pickExtinguishingBarrel1.Items.Add(item.Device);
                pickExtinguishingBarrel2.Items.Add(item.Device);
            }

            foreach (var item in App.DataAccess.NumberOfDevicesRepository.BarrelForDefenseList)
            {
                pickBarrelsForDefense.Items.Add(item.Device);
            }

            if (_document.NumberOfDevices.WaterBarrels != null)
            {
                pickWaterBarrels.SelectedItem = _document.NumberOfDevices.WaterBarrels.ToString();
            }
            if (_document.NumberOfDevices.FoamBarrels != null)
            {
                pickFoamBarrels.SelectedItem = _document.NumberOfDevices.FoamBarrels.ToString();
            }

            if (_document.NumberOfDevices.BarrelsForDefense != null)
            {
                pickBarrelsForDefense.SelectedItem = _document.NumberOfDevices.BarrelsForDefense.ToString();
            }

            if (_document.NumberOfDevices.ExtinguishingBarrel1 != null)
            {
                pickExtinguishingBarrel1.SelectedItem = _document.NumberOfDevices.ExtinguishingBarrel1.ToString();
            }

            if (_document.NumberOfDevices.ExtinguishingBarrel2 != null)
            {
                pickExtinguishingBarrel2.SelectedItem = _document.NumberOfDevices.ExtinguishingBarrel2.ToString();
            }

            if (_document.NumberOfDevices.DefenseBarrel1 != null)
            {
                pickDefenseBarrel1.SelectedItem = _document.NumberOfDevices.DefenseBarrel1.ToString();
            }

            if (_document.NumberOfDevices.DefenseBarrel2 != null)
            {
                pickDefenseBarrel2.SelectedItem = _document.NumberOfDevices.DefenseBarrel2.ToString();
            }

            switch (_document.NumberOfDevices.Method)
            {
                case Data.ExtinguishingMethodType.Water:
                    rbWater.IsChecked = true;
                    break;
                case Data.ExtinguishingMethodType.SurfaceQuenching:
                    rbSurfaceQuenching.IsChecked = true;
                    break;
                case Data.ExtinguishingMethodType.BulkQuenching:
                    rbBulkQuenching.IsChecked = true;
                    break;
                case Data.ExtinguishingMethodType.LVJ:
                    rbLVJ.IsChecked = true;
                    break;
                case Data.ExtinguishingMethodType.GJ:
                    rbGJ.IsChecked = true;
                    break;

                default:
                    break;
            }
            EnabledContorls();
        }

        protected override void OnAppearing()
        {
            Init(); 
            BindingContext = _document.NumberOfDevices;
            base.OnAppearing();
        }

        void OnExtinguishingMethodRadioButtonCheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            RadioButton rb = (RadioButton)sender;
            EnabledContorls();
            if (rb == rbFoam && rbFoam.IsChecked == true)
            {
                _document.NumberOfDevices.Method = Data.ExtinguishingMethodType.None;
            }

            else if (rb == rbSurfaceQuenching && rbSurfaceQuenching.IsChecked == true)
            {
                _document.NumberOfDevices.Method = Data.ExtinguishingMethodType.SurfaceQuenching;
            }

            else if (rb == rbBulkQuenching && rb.IsChecked == true)
            {
                _document.NumberOfDevices.Method = Data.ExtinguishingMethodType.BulkQuenching;
            }
            else if (rb == rbWater && rb.IsChecked == true)
            {
                _document.NumberOfDevices.Method = Data.ExtinguishingMethodType.Water;
            }
            else if (rb == rbLVJ && rb.IsChecked == true)
            {
                _document.NumberOfDevices.Method = Data.ExtinguishingMethodType.LVJ;
            }
            else if (rb == rbGJ && rb.IsChecked == true)
            {
                _document.NumberOfDevices.Method = Data.ExtinguishingMethodType.GJ;
            }
        }

        private void Picker_SelectedBarrel(object sender, EventArgs e)
        {
            Picker picker = (Picker)sender;
            if (picker == pickWaterBarrels && pickWaterBarrels.SelectedIndex != -1)
            {
                _document.NumberOfDevices.WaterBarrels = pickWaterBarrels.Items[pickWaterBarrels.SelectedIndex];

            }
            else if (picker == pickFoamBarrels && pickFoamBarrels.SelectedIndex != -1)
            {
                _document.NumberOfDevices.FoamBarrels = pickFoamBarrels.Items[pickFoamBarrels.SelectedIndex];

            }
            else if (picker == pickBarrelsForDefense && pickBarrelsForDefense.SelectedIndex != -1)
            {
                _document.NumberOfDevices.BarrelsForDefense = pickBarrelsForDefense.Items[pickBarrelsForDefense.SelectedIndex];

            }
            else if (picker == pickExtinguishingBarrel1 && pickExtinguishingBarrel1.SelectedIndex != -1)
            {
                _document.NumberOfDevices.ExtinguishingBarrel1 = pickExtinguishingBarrel1.Items[pickExtinguishingBarrel1.SelectedIndex];

            }
            else if (picker == pickExtinguishingBarrel2 && pickExtinguishingBarrel2.SelectedIndex != -1)
            {
                _document.NumberOfDevices.ExtinguishingBarrel2 = pickExtinguishingBarrel2.Items[pickExtinguishingBarrel2.SelectedIndex];

            }
            else if (picker == pickDefenseBarrel1 && pickDefenseBarrel1.SelectedIndex != -1)
            {
                _document.NumberOfDevices.DefenseBarrel1 = pickDefenseBarrel1.Items[pickDefenseBarrel1.SelectedIndex];

            }
            else if (picker == pickDefenseBarrel2 && pickDefenseBarrel2.SelectedIndex != -1)
            {
                _document.NumberOfDevices.DefenseBarrel2 = pickDefenseBarrel2.Items[pickDefenseBarrel2.SelectedIndex];

            }
        }

        private void BtnClearBarrel_OnClicked(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            if (btn == btnClearExtBarrel1)
            {
                pickExtinguishingBarrel1.SelectedItem = "";
                _document.NumberOfDevices.ExtinguishingBarrel1 = "";
            }
            else if (btn == btnClearExtBarrel2)
            {
                pickExtinguishingBarrel2.SelectedItem = "";
                _document.NumberOfDevices.ExtinguishingBarrel2 = "";
            }
            else if (btn == btnClearFoamBarrel)
            {
                pickFoamBarrels.SelectedItem = "";
                _document.NumberOfDevices.FoamBarrels = "";
            }
            else if (btn == btnClearWaterBarrel)
            {
                pickWaterBarrels.SelectedItem = "";
                _document.NumberOfDevices.WaterBarrels = "";
            }
            else if (btn == btnClearBarrelsForDefense)
            {
                pickBarrelsForDefense.SelectedItem = "";
                _document.NumberOfDevices.BarrelsForDefense = "";
            }
            else if (btn == btnClearDefBarrel1)
            {
                pickDefenseBarrel1.SelectedItem = "";
                _document.NumberOfDevices.DefenseBarrel1 = "";
            }
            else if (btn == btnClearDefBarrel2)
            {
                pickDefenseBarrel2.SelectedItem = "";
                _document.NumberOfDevices.DefenseBarrel2 = "";
            }
        }

        void OnAltSolutionRadioButtonCheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            EnabledContorls();
        }

        private async void BtnUpdate_Clicked(object sender, EventArgs e)
        {
            btnUpdate.IsEnabled = false;
            try
            {
                IsBusy = true;
                if (rbFoam.IsChecked)
                {
                    await DisplayAlert("Ошибка", "Не выбран способ тушения.", "OK");
                }
                else
                {
                    _document.NumberOfDevices.UpdateParams();
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ошибка", ex.Message, "OK");
            }
            finally
            {
                btnUpdate.IsEnabled = true;
                IsBusy = false;
            }
        }
        private async void BtnCancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}