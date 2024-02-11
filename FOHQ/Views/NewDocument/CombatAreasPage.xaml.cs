using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using FOHQ.Models;
using FOHQ.Data;

namespace FOHQ.Views.NewDocument
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CombatAreasPage : ContentPage
    {
        private readonly Document _document;
        private readonly IList<CombatArea> _combatAreas = new ObservableCollection<CombatArea>();

        public CombatAreasPage(Document document)
        {
            InitializeComponent();

            _document = document ?? throw new ArgumentNullException(nameof(document));
        }

        protected override void OnAppearing()
        {
            _combatAreas.Clear();
            foreach (var combatArea in _document.CombatAreas)
            {
                _combatAreas.Add(combatArea);
            }
            listCombatAreas.ItemsSource = _combatAreas;

            // выводим количество л/с и стволов с учётом возможных изменений на боевых участках
            var query = _combatAreas.ToList().GroupBy(
                x => x.CombatAreaName,
                (name, areas) =>
                {
                    var combatAreas = areas.ToList();
                    return new
                    {
                        Key = name,
                        Last = combatAreas.Last(),
                        BarrelsRs50 = combatAreas.Max(x => x.BarrelsRs50),
                        BarrelsRs70 = combatAreas.Max(x => x.BarrelsRs70),
                        BarrelsVarFlow = combatAreas.Max(x => x.BarrelsVarFlow),
                        BarrelsL = combatAreas.Max(x => x.BarrelsL),
                        BarrelsGps = combatAreas.Max(x => x.BarrelsGps),
                        BarrelsSvp = combatAreas.Max(x => x.BarrelsSvp),
                        BarrelsOther = combatAreas.Max(x => x.BarrelsOther)
                    };
                }).ToList();

            lblTotalPersonnel.Text = query.Sum(x => x.Last.Personnel).ToString();
            lblTotalDepartments.Text = query.Sum(x => x.Last.Departments).ToString();
            lblTotalGasUnits.Text = query.Sum(x => x.Last.GasUnits).ToString();
            lblTotalBarrelsRs50.Text = query.Sum(x => x.BarrelsRs50).ToString();
            lblTotalBarrelsRs70.Text = query.Sum(x => x.BarrelsRs70).ToString();
            lblTotalBarrelsVarFlow.Text = query.Sum(x => x.BarrelsVarFlow).ToString();
            lblTotalBarrelsL.Text = query.Sum(x => x.BarrelsL).ToString();
            lblTotalBarrelsGps.Text = query.Sum(x => x.BarrelsGps).ToString();
            lblTotalBarrelsSvp.Text = query.Sum(x => x.BarrelsSvp).ToString();
            lblTotalBarrelsOther.Text = query.Sum(x => x.BarrelsOther).ToString();

            base.OnAppearing();
        }

        private async void BtnAddCombatArea_Clicked(object sender, EventArgs e)
        {
            if (IsBusy) return;

            try
            {
                IsBusy = true;
                await App.DataAccess.DocumentRepository.SaveItemAsync(_document);
                await Navigation.PushModalAsync(new AddCombatAreaPage(_document));
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

        private async void ItemEditCombatArea_Clicked(object sender, EventArgs e)
        {
            if (IsBusy) return;

            var item = (MenuItem)sender;
            if (!(item.CommandParameter is CombatArea combatArea)) return;

            try
            {
                IsBusy = true;
                await Navigation.PushModalAsync(new AddCombatAreaPage(_document, combatArea));
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

        private async void ItemDeleteCombatArea_Clicked(object sender, EventArgs e)
        {
            if (IsBusy) return;

            var item = (MenuItem)sender;
            if (!(item.CommandParameter is CombatArea combatArea)) return;

            if (await DisplayAlert(
                "Удаление", "Удалить выбранную запись?", "Да", "Нет"))
            {
                try
                {
                    IsBusy = true;

                    _document.CombatAreas.Remove(combatArea);
                    await App.DataAccess.DocumentRepository.SaveItemAsync(_document);
                    _combatAreas.Remove(combatArea);
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