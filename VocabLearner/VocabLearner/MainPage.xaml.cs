﻿using MySqlX.XDevAPI;
using Org.BouncyCastle.Utilities.Encoders;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using VocabLearner.Controller;
using Xamarin.Forms;

namespace VocabLearner
{
    public partial class MainPage : ContentPage
    {

        public static UserDB database = new UserDB();


        public MainPage()
        {
            
            
            InitializeComponent();
           
        }

        private object TranslateAPI()
        {
            throw new NotImplementedException();
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

                User loggedInUser = await database.Login(usernameEntry.Text, passwordEntry.Text);

                if (loggedInUser != null) //if a loggedInUser is present
                {
                    // set username and profile pic to userdefaults
                    App.loggedInUser = Convert.ToString(usernameEntry.Text); //set the current logged in user
                    App.profilePic = loggedInUser.profilePic; //set te profile pic
                    await Navigation.PushAsync(new HomeTab());
                }
                else
                {
                    await DisplayAlert("Failed!", "User not found", "Ok");

                }


            }
            else
            {
                await DisplayAlert("Error!", "Please enter your username and password", "Ok");
                Console.WriteLine(App.loggedInUser);
            }
        }
    }
}
