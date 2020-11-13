using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.ComponentModel; 
using System.Runtime.CompilerServices;
using Java.IO;
using VocabLearner.Controller;

namespace VocabLearner.MainViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditPage : ContentPage, INotifyPropertyChanged
    {
        private string currentSearchedWord;
        private Word currentWordPair;

        private Word transWord;
        public Word TransWordBind
        {
            get => transWord;
            set
            {
                if (transWord != value)
                {
                    transWord = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public EditPage()
        {

            Word wordPair = new Word();
            wordPair.sourceWord = "";
            wordPair.translatedWord = "";
            TransWordBind = wordPair;

            BindingContext = wordPair;
            InitializeComponent();
        }

        private async void Recently_Added_OnClickedAsync(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RecentlyAddedView());
        }
        private async void Delete_OnClickedAsync(object sender, EventArgs e)
        {
            if (currentSearchedWord != null) //currentSearchedWord only contains the most current word that is in the database
            {

                _ = await App.Database.DeleteWordsAsync(currentWordPair); //deletes the word from the database
                await DisplayAlert("Sucess!", currentWordPair.sourceWord + " = " + currentWordPair.translatedWord + " has now been deleted", "Ok");
                currentWordPair = null; //refresh current word pair
                BindingContext = null; // and bindings
            }
        }
        private async void Edit_OnClickedAsync(object sender, EventArgs e)
        {
            if (currentSearchedWord != null) //currentSearchedWord only contains the most current word that is in the database
            {
                Word newWord = new Word();
                newWord.sourceWord = sourceWord.Text;
                newWord.translatedWord = translatedWord.Text;
                _ = await App.Database.DeleteWordsAsync(currentWordPair);
                _ = await App.Database.SaveWordsAsync(newWord);

                await DisplayAlert("Sucess!", newWord.sourceWord + " = " + newWord.translatedWord + " has now been saved", "Ok");
                currentWordPair = null; // refresh the current word pair
                BindingContext = null; //reset the bindings

            }
        }
        private async void Search_OnClickedAsync(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(searchWord.Text))
            {
                //TransWordBind = await GetWords();
                BindingContext = await GetWords();
            }
        }

        private async Task<Word> GetWords()
        {
            Word newWord = new Word();
            newWord.sourceWord = searchWord.Text;
            newWord.translatedWord = await App.Database.GetOneWord(searchWord.Text);

            if(newWord.translatedWord != null) //if the response from the search is in the database
            {
                currentSearchedWord = searchWord.Text; //set the current searched word to be the users searched word
                currentWordPair = newWord;
                return newWord;
            } 
            else //otherwise
            {
                currentSearchedWord = null; //set to null so user doesnt accidently edit /delete the previously added word
            }


            return null;
        }


        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            System.Console.WriteLine("Method is called");
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}