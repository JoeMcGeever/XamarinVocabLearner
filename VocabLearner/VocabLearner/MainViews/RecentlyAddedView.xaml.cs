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



         //   Word newWord = new Word()
         //   {
         //       sourceWord = "testsource",
         //       translatedWord = "testTrans"
         //   };

         //   recentController.AddNewWord(newWord);


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

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;

            await DisplayAlert("Item Tapped", "An item was tapped.", "OK");

            //Deselect Item
            ((ListView)sender).SelectedItem = null;
        }
    }
}
