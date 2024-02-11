using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using FOHQ.Models;
using System.Threading.Tasks;

namespace FOHQ.Views.NewDocument
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FilingMethodsPage : ContentPage
    {
        private readonly Document _document;

        public FilingMethodsPage(Document document)
        {
            InitializeComponent();
            _document = document ?? throw new ArgumentNullException(nameof(document));
        }

        private void Init()
        {
            foreach (var item in App.DataAccess.FilingMethodsRepository.AscentDescentTypeList)
            {
                pickAscentDescentLocality.Items.Add(item.Name);
                pickAscentDescentTrunk.Items.Add(item.Name);
            }

            pickAscentDescentLocality.SelectedIndex = (int)_document.FilingMethods.AscentDescentLocality;
            pickAscentDescentTrunk.SelectedIndex = (int)_document.FilingMethods.AscentDescentTrunk;

            foreach (var item in App.DataAccess.FilingMethodsRepository.PressureOnPumpList)
            {
                pickPressureOnPump.Items.Add(Convert.ToString(item));
            }

            if (_document.FilingMethods.PressureOnPump != 0)
            {
                pickPressureOnPump.SelectedItem = Convert.ToString(_document.FilingMethods.PressureOnPump);
            }

            rbWaterSupplyMethod.IsChecked = _document.FilingMethods.IsWaterSupplyMethod;
            rbWaterSupplyToPumpMethod.IsChecked = _document.FilingMethods.IsWaterSupplyToPumpMethod;

            EnabledControls();
            return;
        }

        protected override void OnAppearing()
        {
            Init(); 

            BindingContext = _document.FilingMethods;
            base.OnAppearing();
        }

        void OnMethodRadioButtonCheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            RadioButton rb = (RadioButton)sender;

            EnabledControls();

            if (rb == rbWaterSupplyMethod)
            {
                _document.FilingMethods.IsWaterSupplyMethod = rbWaterSupplyMethod.IsChecked;
            }
            else if (rb == rbWaterSupplyToPumpMethod)
            {
                _document.FilingMethods.IsWaterSupplyToPumpMethod = rbWaterSupplyToPumpMethod.IsChecked;
            }
        }

        private void BtnClearPressureOnPump_OnClicked(object sender, EventArgs e)
        {
            pickPressureOnPump.SelectedItem = "";
            _document.FilingMethods.PressureOnPump = 0;
        }


        private void Picker_SelectedPressureOnPump(object sender, EventArgs e)
        {
            if (pickPressureOnPump.SelectedIndex != -1)
            {
                _document.FilingMethods.PressureOnPump = Convert.ToInt32(pickPressureOnPump.Items[pickPressureOnPump.SelectedIndex]);
            }
        }

        private void Picker_SelectedAscentDescent(object sender, EventArgs e)
        {
            Picker picker = (Picker)sender;
            FOHQ.Data.AscentDescentTypeItem itemAscentDescent = null;
            if (picker.SelectedIndex != -1)
            {
                foreach (FOHQ.Data.AscentDescentTypeItem item in App.DataAccess.FilingMethodsRepository.AscentDescentTypeList)
                {
                    if (picker.SelectedIndex == ((int)item.Type))
                    {
                        itemAscentDescent = item;
                    }
                }
                if (itemAscentDescent != null)
                {
                    if (picker == pickAscentDescentLocality)
                    {
                        _document.FilingMethods.AscentDescentLocality = itemAscentDescent.Type;

                    }
                    else if (picker == pickAscentDescentTrunk)
                    {
                        _document.FilingMethods.AscentDescentTrunk = itemAscentDescent.Type;
                    }
                }
            }

        }

        private void EnabledControls()
        {
            panelWaterSupplyMethod.IsEnabled = rbWaterSupplyMethod.IsChecked;
            panelWaterSupplyToPumpMethod.IsEnabled = rbWaterSupplyToPumpMethod.IsChecked;
        }
        private async void BtnCancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}