using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VocabLearner
{
    public partial class App : Application
    {
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
