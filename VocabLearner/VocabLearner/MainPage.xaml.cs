using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace VocabLearner
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void NavigateButton_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SignUpPage());
        }

        private async void Login_OnClicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(usernameEntry.Text) //if the username entry field
            && !string.IsNullOrWhiteSpace(passwordEntry.Text)) //or password ones are not empty
            {
                //set username as userdefaults?
                await DisplayAlert("Success!", "You are logged in", "Ok");
                App.loggedInUser = Convert.ToString(usernameEntry.Text);
                //await Navigation.PushAsync(new HomePage());
            }
            else
            {
                await DisplayAlert("Error!", "Please enter your username and password", "Ok");
                Console.WriteLine(App.loggedInUser);
            }
        }
    }
}
