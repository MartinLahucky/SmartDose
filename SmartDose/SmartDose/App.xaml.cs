using System;
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

        public static DatabaseController Database
        {
            get
            {
                if (database == null)
                {
                    database = new DatabaseController();
                }
                return database;
            }
        }
        public App(String databaseLocation)
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage())
            {
                BarTextColor = Color.FromHex("#FFFFFF"),
            };
            DatabaseLocation = databaseLocation;
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