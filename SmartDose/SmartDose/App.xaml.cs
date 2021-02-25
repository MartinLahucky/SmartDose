using SmartDose.Helpers.Database_Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace SmartDose
{
    public partial class App : Application
    {
        private static DatabaseController database;
        public static string DatabaseLocation = string.Empty;

        public App(string databaseLocation)
        {
            InitializeComponent();

            DatabaseLocation = databaseLocation;
            MainPage = new NavigationPage(new SwitchPage())
            {
                BarTextColor = Color.FromHex("#FFFFFF")
            };
        }

        public static DatabaseController Database
        {
            get
            {
                if (database == null) database = new DatabaseController();
                return database;
            }
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