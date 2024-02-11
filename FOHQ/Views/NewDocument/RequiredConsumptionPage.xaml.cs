using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using FOHQ.Models;
using FOHQ.Data;

namespace FOHQ.Views.NewDocument
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RequiredConsumptionPage : ContentPage
    {
        private readonly Document _document;

        public RequiredConsumptionPage(Document document)
        {
            InitializeComponent();
            _document = document ?? throw new ArgumentNullException(nameof(document));
        }

        protected override void OnAppearing()
        {
            foreach (var item in App.DataAccess.RequiredConsumptionRepository.SectionByObjectsList)
            {
                pickByObject.Items.Add(item.Name);
            }
            if (_document.RequiredConsumption.SectionByObject != Data.SectionByObjectsType.None)
            {
                pickByObject.SelectedIndex = (int)_document.RequiredConsumption.SectionByObject;
                Fill_SectionByDestination();
            }
            switch (_document.RequiredConsumption.ExtinguishingType)
            {
                case Data.ExtinguishingType.ExtinguishingFront:
                    rbExtinguishingFront.IsChecked = true;
                    break;

                case Data.ExtinguishingType.ExtinguishingPerimeter:
                    rbExtinguishingPerimeter.IsChecked = true;
                    break;

                default:
                    break;
            }

            if (pickByObject.SelectedIndex != -1 && _document.RequiredConsumption.SectionByDestination != null)
            {
                pickByDestination.SelectedItem = _document.RequiredConsumption.SectionByDestination.ToString();
            }

            BindingContext = _document.RequiredConsumption;
            base.OnAppearing();
        }

        private void BtnClearByObject_OnClicked(object sender, EventArgs e)
        {
            pickByObject.SelectedItem = "";
            pickByDestination.IsEnabled = false;
            _document.RequiredConsumption.SectionByObject = Data.SectionByObjectsType.None;
        }

        private void BtnClearDestination_OnClicked(object sender, EventArgs e)
        {
            pickByDestination.SelectedItem = "";
            _document.RequiredConsumption.SectionByDestination = "";
        }

        private void Picker_SelectedByObject(object sender, EventArgs e)
        {
            Fill_SectionByDestination();
        }

        void OnRadioButtonCheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            RadioButton rb = (RadioButton)sender;
            entryConsumptionExtinguishing.IsEnabled = true;
            if (rb == rbExtinguishingFront)
            {
                _document.RequiredConsumption.ExtinguishingType = Data.ExtinguishingType.ExtinguishingFront;
            }
            else if (rb == rbExtinguishingPerimeter)
            {
                _document.RequiredConsumption.ExtinguishingType = Data.ExtinguishingType.ExtinguishingPerimeter;
            }
        }

        void Fill_SectionByDestination()
        {
            foreach (FOHQ.Data.SectionByObjects section in App.DataAccess.RequiredConsumptionRepository.SectionByObjectsList)
            {
                if (pickByObject.SelectedIndex == ((int)section.Section))
                {
                    _document.RequiredConsumption.SectionByObject = section.Section;
                    pickByDestination.Items.Clear();
                    List<SectionByDestinations> listItems;
                    if (_document.RequiredConsumption.SectionByObject == FOHQ.Data.SectionByObjectsType.Buildings)
                    {
                        listItems = App.DataAccess.RequiredConsumptionRepository.SectionDestinationsBuildings;
                    }
                    else if (_document.RequiredConsumption.SectionByObject == FOHQ.Data.SectionByObjectsType.Liquids)
                    {
                        listItems = App.DataAccess.RequiredConsumptionRepository.SectionDestinationsLiquids;
                    }
                    else if (_document.RequiredConsumption.SectionByObject == FOHQ.Data.SectionByObjectsType.Materials)
                    {
                        listItems = App.DataAccess.RequiredConsumptionRepository.SectionDestinationsMaterials;
                    }
                    else if (_document.RequiredConsumption.SectionByObject == FOHQ.Data.SectionByObjectsType.Transport)
                    {
                        listItems = App.DataAccess.RequiredConsumptionRepository.SectionDestinationsTransport;
                    }
                    else
                    {
                        listItems = null;
                    }
                    if (listItems != null)
                    {
                        foreach (var item in listItems)
                        {
                            pickByDestination.Items.Add(item.Name);
                        }
                        pickByDestination.IsEnabled = true;
                    }
                    break;
                }
            }
        }
        private void Picker_SelectedByDestination(object sender, EventArgs e)
        {
            if (pickByDestination.SelectedIndex != -1)
            {
                _document.RequiredConsumption.SectionByDestination = pickByDestination.Items[pickByDestination.SelectedIndex];
            }
        }
        private async void BtnCancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}