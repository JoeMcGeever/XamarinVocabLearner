using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VocabLearner
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignUpPage : ContentPage
    {
        public SignUpPage()
        {
            InitializeComponent();
        }


        private void LogIn_OnClicked(object sender, EventArgs e)
        {
            //await Navigation.PushAsync(new SignUpPage());
            // Perhaps there is a way to deque current page back to previous
            Application.Current.MainPage.Navigation.PopAsync();
        }



        private async void SignUp_OnClicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(usernameEntry.Text) //if the username entry field
            && !string.IsNullOrWhiteSpace(passwordEntry.Text)) //or password ones are not empty
            {
                //save to xampp database here

                await Application.Current.MainPage.Navigation.PopAsync(); //pop this view off the current navigation stack
            }
            else
            {
                await DisplayAlert("Error!", "Please enter all of your details", "Ok");
            }

        }
    }


 }