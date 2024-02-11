using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using FOHQ.Data;
using FOHQ.Models;

namespace FOHQ.Views.NewDocument
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FireUnitsPage : ContentPage
    {
        private readonly Document _document;
        private readonly IList<FireUnit> _fireUnits = new ObservableCollection<FireUnit>();

        public FireUnitsPage(Document document)
        {
            InitializeComponent();

            _document = document ?? throw new ArgumentNullException(nameof(document));
        }

        protected override void OnAppearing()
        {
            _fireUnits.Clear();
            foreach (var fireUnit in _document.FireUnits)
            {
                _fireUnits.Add(fireUnit);
            }
            listFireUnits.ItemsSource = _fireUnits;

            lblTotalPersonnel.Text = _fireUnits.Sum(x => x.Personnel).ToString();

            base.OnAppearing();
        }

        private async void BtnAddFireForcesAndMeans_Clicked(object sender, EventArgs e)
        {
            if (IsBusy) return;

            try
            {
                IsBusy = true;
                await App.DataAccess.DocumentRepository.SaveItemAsync(_document);
                await Navigation.PushModalAsync(new AddFireUnitPage(_document));
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

        private async void ItemEditFireUnits_Clicked(object sender, EventArgs e)
        {
            if (IsBusy) return;

            var item = (MenuItem)sender;
            if (!(item.CommandParameter is FireUnit fireUnit)) return;

            try
            {
                IsBusy = true;
                await Navigation.PushModalAsync(new AddFireUnitPage(_document, fireUnit));
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

        private async void ItemDeleteFireUnits_Clicked(object sender, EventArgs e)
        {
            if (IsBusy) return;

            var item = (MenuItem)sender;
            if (!(item.CommandParameter is FireUnit fireUnit)) return;

            if (await DisplayAlert(
                "Удаление", "Удалить выбранную запись?", "Да", "Нет"))
            {
                try
                {
                    IsBusy = true;

                    _document.FireUnits.Remove(fireUnit);
                    await App.DataAccess.DocumentRepository.SaveItemAsync(_document);
                    _fireUnits.Remove(fireUnit);
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