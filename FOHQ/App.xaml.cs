using System.IO;

using Xamarin.Forms;
using Xamarin.Essentials;

using FOHQ.Data;
using System.Globalization;

namespace FOHQ
{
    public partial class App : Application
    {
        private const string DbName = "documents.db3";

        private static DataAccess _dataAccess;
        public static DataAccess DataAccess
        {
            get
            {
                return _dataAccess ??= new DataAccessFactory().Create(Path.Combine(FileSystem.AppDataDirectory, DbName));
            }
        }

        public App()
        {
            //необходимо перевести с русской раскладки на английскую, для корректной работы неанглийского андроида
            var userSelectedCulture = new CultureInfo("en-US");
            userSelectedCulture.NumberFormat.NumberDecimalSeparator = ".";

            System.Threading.Thread.CurrentThread.CurrentCulture = userSelectedCulture;
            System.Threading.Thread.CurrentThread.CurrentUICulture = userSelectedCulture;
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
