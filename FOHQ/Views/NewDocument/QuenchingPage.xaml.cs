using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using FOHQ.Models;

namespace FOHQ.Views.NewDocument
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class QuenchingPage : ContentPage
    {
        private readonly Document _document;

        public QuenchingPage(Document document)
        {
            InitializeComponent();
            _document = document ?? throw new ArgumentNullException(nameof(document));
        }

        void OnQuenchingTrunkRadioButtonCheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            RadioButton rb = (RadioButton)sender;
            if (rb == rbHandTrunk)
            {
                _document.Quenching.QuenchingTrunk = Data.QuenchingBarrelType.HandTrunk;
            }
            else if (rb == rbWaterCannon)
            {
                _document.Quenching.QuenchingTrunk = Data.QuenchingBarrelType.WaterCannon;
            }
        }

        void OnFormOfQuenchingTypeRadioButtonCheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            RadioButton rb = (RadioButton)sender;

            EnabledControls();

            if (rb == rbQuadrant)
            {
                _document.Quenching.Shape = Data.FormOfQuenchingTypes.Quadrant;
            }
            else if (rb == rbSemicircle)
            {
                _document.Quenching.Shape = Data.FormOfQuenchingTypes.Semicircle;
            }
            else if (rb == rbCicle)
            {
                _document.Quenching.Shape = Data.FormOfQuenchingTypes.Cicle;
            }
            else if (rb == rbRectangleOneWay)
            {
                _document.Quenching.Shape = Data.FormOfQuenchingTypes.RectangleOneWay;
            }
            else if (rb == rbRectangleTwoWayLeft)
            {
                _document.Quenching.Shape = Data.FormOfQuenchingTypes.RectangleTwoWayLeft;
            }
            else if (rb == rbRectangleTwoWayRigt)
            {
                _document.Quenching.Shape = Data.FormOfQuenchingTypes.RectangleTwoWayRigt;
            }
            else if (rb == rbRectangleThirdWayLeft)
            {
                _document.Quenching.Shape = Data.FormOfQuenchingTypes.RectangleThirdWayLeft;
            }
            else if (rb == rbRectangleThirdWayRight)
            {
                _document.Quenching.Shape = Data.FormOfQuenchingTypes.RectangleThirdWayRight;
            }
            else if (rb == rbRectangleFourWay)
            {
                _document.Quenching.Shape = Data.FormOfQuenchingTypes.RectangleFourWay;
            }

        }

        private void EnabledControls()
        {
            if (rbRectangleThirdWay.IsChecked || rbRectangleThirdWayRight.IsChecked || rbRectangleThirdWayLeft.IsChecked)
            {
                rbRectangleThirdWayLeft.IsEnabled = true;
                rbRectangleThirdWayRight.IsEnabled = true;
            }
            else
            {
                if (rbRectangleThirdWay.IsChecked)
                {
                    rbRectangleThirdWayLeft.IsEnabled = true;
                    rbRectangleThirdWayRight.IsEnabled = true;
                }
                else
                {
                    rbRectangleThirdWayLeft.IsEnabled = false;
                    rbRectangleThirdWayRight.IsEnabled = false;
                }
            }

            if (rbRectangleTwoWay.IsChecked || rbRectangleTwoWayLeft.IsChecked || rbRectangleTwoWayRigt.IsChecked)
            {
                rbRectangleTwoWayLeft.IsEnabled = true;
                rbRectangleTwoWayRigt.IsEnabled = true;
            }
            else
            {
                if (rbRectangleTwoWay.IsChecked)
                {
                    rbRectangleTwoWayLeft.IsEnabled = true;
                    rbRectangleTwoWayRigt.IsEnabled = true;
                }
                else
                {
                    rbRectangleTwoWayLeft.IsEnabled = false;
                    rbRectangleTwoWayRigt.IsEnabled = false;
                }
            }
        }

        protected override void OnAppearing()
        {
            BindingContext = _document.Quenching;

            if (_document.Quenching.Shape == Data.FormOfQuenchingTypes.Quadrant)
            {
                rbQuadrant.IsChecked = true;
            }
            else if (_document.Quenching.Shape == Data.FormOfQuenchingTypes.Semicircle)
            {
                rbSemicircle.IsChecked = true;
            }
            if (_document.Quenching.Shape == Data.FormOfQuenchingTypes.Cicle)
            {
                rbCicle.IsChecked = true;
            }
            else if (_document.Quenching.Shape == Data.FormOfQuenchingTypes.RectangleOneWay)
            {
                rbRectangleOneWay.IsChecked = true;
            }
            if (_document.Quenching.Shape == Data.FormOfQuenchingTypes.RectangleTwoWayLeft)
            {
                rbRectangleTwoWayLeft.IsChecked = true;
            }
            else if (_document.Quenching.Shape == Data.FormOfQuenchingTypes.RectangleTwoWayRigt)
            {
                rbRectangleTwoWayRigt.IsChecked = true;
            }
            if (_document.Quenching.Shape == Data.FormOfQuenchingTypes.RectangleThirdWayLeft)
            {
                rbRectangleThirdWayLeft.IsChecked = true;
            }
            else if (_document.Quenching.Shape == Data.FormOfQuenchingTypes.RectangleThirdWayRight)
            {
                rbRectangleThirdWayRight.IsChecked = true;
            }
            else if (_document.Quenching.Shape == Data.FormOfQuenchingTypes.RectangleFourWay)
            {
                rbRectangleFourWay.IsChecked = true;
            }

            if (_document.Quenching.QuenchingTrunk == Data.QuenchingBarrelType.HandTrunk)
            {
                rbHandTrunk.IsChecked = true;
            }
            else if (_document.Quenching.QuenchingTrunk == Data.QuenchingBarrelType.WaterCannon)
            {
                rbWaterCannon.IsChecked = true;
            }
            EnabledControls();
            base.OnAppearing();
        }

        private async void BtnUpdate_Clicked(object sender, EventArgs e)
        {
            btnUpdate.IsEnabled = false;
            try
            {
                IsBusy = true;
                if (rbRectangleTwoWay.IsChecked || rbRectangleThirdWay.IsChecked)
                {
                    await DisplayAlert("Ошибка", "Не выбрана форма пожара.", "OK");
                }
                else
                {
                    _document.Quenching.UpdateParams();
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