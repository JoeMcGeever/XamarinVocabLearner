using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VocabLearner.Controller;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VocabLearner.MainViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddPage : ContentPage
    {
        public AddPage()
        {
            InitializeComponent();
        }

        private async void Add_OnClicked(object sender, EventArgs e)
        {

            //when the add button is clicked, the entries are checked whether they are empty first, then if it already exists on the system, and then adds the word to the dB and to recently added file
            if (string.IsNullOrWhiteSpace(sourceWord.Text) //if the source word entry field
            | string.IsNullOrWhiteSpace(translatedWord.Text)) //or translated ones are empty
            {
                await DisplayAlert("Error!", "Please enter all of your details", "Ok");
                return;
            }


            var wordPair = await App.Database.GetOneWord(sourceWord.Text);

            if (wordPair != null)//if already in the system
            {
                await DisplayAlert("Error!", "The word " + sourceWord.Text + " already exists on the system (see edit tab for more details)", "Ok");
            }
            else
            {
                await App.Database.SaveWordsAsync(new Word //add the new word to the dB
                {
                    sourceWord = sourceWord.Text,
                    translatedWord = translatedWord.Text
                });

                await DisplayAlert("Success!", "Word Added", "Ok");


            }
            
        }
    }
}