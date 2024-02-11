using FOHQ.Models;
using FOHQ.Views.NewDocument;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FOHQ.Views.Main
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewDocumentPage : TabbedPage
    {
        private readonly Document _document;

        public NewDocumentPage(Document document = null)
        {
            InitializeComponent();

            _document = document ?? new Document();

            Children.Add(new DocumentHeaderPage(_document));
            Children.Add(new FireUnitsPage(_document));
            Children.Add(new CombatAreasPage(_document));
            Children.Add(new OrdersPage(_document));
            Children.Add(new ForcesAndMeansCalculationPage(_document));
        }

        protected override bool OnBackButtonPressed()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                if (await DisplayAlert(
                    "Закрытие", "Закрыть текущий документ?", "Да", "Нет"))
                {
                    await Navigation.PopModalAsync();
                }
            });

            return true;
        }
    }
}