using System;
using System.IO;
using Xamarin.Forms;

namespace VocabLearner
{
    public partial class App : Application
    {
        static LocalDB database;

        public static LocalDB Database
        {
            get
            {
                if (database == null)
                {
                    database = new LocalDB(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "words.db3"));
                }
                return database;
            }
        }
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());
        }

        public static string loggedInUser //the current logged in user
        {
            get; set;
        }


        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
