using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VocabLearner.Controller;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VocabLearner.Games
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GamePage : ContentPage
    {
        public  GamePage()
        {
            InitializeComponent();
        }

        private async void Play_OnClickedAsync(object sender, EventArgs e)
        {
            var listOfQuestions = await App.Database.GetTenWordsAsync();

            if (listOfQuestions == null)
            {
                await DisplayAlert("Cannot Play!", "You need at least 10 words to play", "Ok");

                await Application.Current.MainPage.Navigation.PopAsync(); //return to prior page
            }

            await Navigation.PushAsync(new QuestionPage(listOfQuestions)); //navigates to the question page, sending the list of questions with it

        }
    }
}