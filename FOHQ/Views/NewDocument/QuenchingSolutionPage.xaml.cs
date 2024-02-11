using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using FOHQ.Models;
using FOHQ.Data;

namespace FOHQ.Views.NewDocument
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class QuenchingSolutionPage : ContentPage
    {
        private readonly Document _document;
        public QuenchingSolutionPage(Document document)
        {
            InitializeComponent();
            _document = document ?? throw new ArgumentNullException(nameof(document));
        }

        protected override void OnAppearing()
        {
            switch (_document.QuenchingSolution.Multiplicity)
            {
                case Data.MultiplicityType.Low:
                    rbLow.IsChecked = true;
                    break;
                case Data.MultiplicityType.Medium:
                    rbMedium.IsChecked = true;
                    break;
                default:
                    break;
            }

            switch (_document.QuenchingSolution.LiquorType)
            {
                case Data.LiquorType.LVJ:
                    rbLVJ.IsChecked = true;
                    break;
                case Data.LiquorType.GJ:
                    rbGJ.IsChecked = true;
                    break;
                default:
                    break;
            }

            if (_document.QuenchingSolution.EstimateTime == 10)
            {
                rb10.IsChecked = true;
            }
            else if (_document.QuenchingSolution.EstimateTime == 15)
            {
                rb15.IsChecked = true;
            }

            foreach (var item in App.DataAccess.QuenchingSolutionRepository.DiameterHoseLIst)
            {
                pickHoseDiameter.Items.Add(Convert.ToString(item.Diameter));
            }
            if (_document.QuenchingSolution.HoseDiameter != 0)
            {
                pickHoseDiameter.SelectedItem = Convert.ToString(_document.QuenchingSolution.HoseDiameter);
            }
            if (_document.QuenchingSolution.FoamBarrels != "")
            {
                pickFoamBarrels.SelectedItem = _document.QuenchingSolution.FoamBarrels;
            }
            EnabledContorls();
            BindingContext = _document.QuenchingSolution;
            base.OnAppearing();
        }

        private void EnabledContorls()
        {
            panelMultiplicity.IsEnabled = _document.NumberOfDevices.Method == Data.ExtinguishingMethodType.SurfaceQuenching || _document.NumberOfDevices.Method == Data.ExtinguishingMethodType.BulkQuenching
                || _document.NumberOfDevices.Method == Data.ExtinguishingMethodType.LVJ || _document.NumberOfDevices.Method == Data.ExtinguishingMethodType.GJ;
            panelLVJType.IsEnabled = rbLow.IsChecked || rbMedium.IsChecked;
        }

        void OnMultiplicityTypeRadioButtonCheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            RadioButton rb = (RadioButton)sender;
            pickFoamBarrels.Items.Clear();
            List<MultiplicityFoamBarrelItem> listItems = null;
            if (rb == rbLow)
            {
                if (rb.IsChecked == true)
                {
                    _document.QuenchingSolution.Multiplicity = Data.MultiplicityType.Low;
                    listItems = App.DataAccess.QuenchingSolutionRepository.FoamBarrelLowList;
                }

            }
            else if (rb == rbMedium)
            {
                if (rb.IsChecked == true)
                {
                    _document.QuenchingSolution.Multiplicity = Data.MultiplicityType.Medium;
                    listItems = App.DataAccess.QuenchingSolutionRepository.FoamBarrelMediumList;
                }
            }

            if (listItems != null)
            {
                foreach (var Item in listItems)
                {
                    pickFoamBarrels.Items.Add(Item.Device);
                }
            }
            EnabledContorls();
        }

        void OnEstimateTimeRadioButtonCheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            RadioButton rb = (RadioButton)sender;
            if (rb == rb10)
            {
                if (rb.IsChecked == true)
                {
                    _document.QuenchingSolution.EstimateTime = 10;
                }

            }
            else if (rb == rb15)
            {
                if (rb.IsChecked == true)
                {
                    _document.QuenchingSolution.EstimateTime = 15;
                }
            }
        }

        void OnLiquorTypeRadioButtonCheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            RadioButton rb = (RadioButton)sender;
            if (rb == rbLVJ)
            {
                if (rb.IsChecked == true)
                {
                    _document.QuenchingSolution.LiquorType = Data.LiquorType.LVJ;
                }

            }
            else if (rb == rbGJ)
            {
                if (rb.IsChecked == true)
                {
                    _document.QuenchingSolution.LiquorType = Data.LiquorType.GJ;
                }
            }
        }

        private void BtnClearHoseDiameter_OnClicked(object sender, EventArgs e)
        {
            pickHoseDiameter.SelectedItem = "";
            _document.QuenchingSolution.HoseDiameter = 0;
        }

        private void Picker_SelectedHoseDiameter(object sender, EventArgs e)
        {
            if (pickHoseDiameter.SelectedIndex != -1)
            {
                _document.QuenchingSolution.HoseDiameter = System.Convert.ToInt32(pickHoseDiameter.Items[pickHoseDiameter.SelectedIndex]);
            }
        }

        private void BtnClearFoamBarrels_OnClicked(object sender, EventArgs e)
        {
            pickFoamBarrels.SelectedItem = "";
            _document.QuenchingSolution.FoamBarrels = "";
        }

        private void Picker_SelectedFoamBarrels(object sender, EventArgs e)
        {
            if (pickFoamBarrels.SelectedIndex != -1)
            {
                _document.QuenchingSolution.FoamBarrels = pickFoamBarrels.Items[pickFoamBarrels.SelectedIndex];
            }
        }

        private async void BtnCancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

    }
}