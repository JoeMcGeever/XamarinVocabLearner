using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using VocabLearner.Controller;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VocabLearner.MainViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RecentlyAddedView : ContentPage
    {
        public ObservableCollection<Word> Items = new ObservableCollection<Word>();

        public RecentlyAddedView()
        {
            InitializeComponent();
            List<Word> recentWords = RecentlyAdded.GetAllWords(); //get recently added words
            if (recentWords != null) //if the list isn't null
            {
                for (int i = 0; i < recentWords.Count; i++) //iterate through
                {
                    Items.Add(recentWords[i]);
                    //Items.Add("Source: " + recentWords[i].sourceWord + " - Translation: " + recentWords[i].translatedWord); //append to the list of items to be diplayed
                }
            }
            else
            {
                Console.WriteLine("No words available");
            }
            MyListView.ItemsSource = Items;
        }

        private async void OnDelete(object sender, EventArgs e)
        {
            var mi = ((MenuItem)sender);
            try
            {
                Word wordToDelete = (Word)mi.CommandParameter;

                _ = await App.Database.DeleteWordsAsync(wordToDelete); //deletes the word from the database

                await DisplayAlert("Deleted", wordToDelete.sourceWord + " - " + wordToDelete.translatedWord + " delete context action", "OK");

            } catch
            {
                await DisplayAlert("Error", "Error deleting word, try searching in edit page instead", "OK");

            }

        }
    }
}
