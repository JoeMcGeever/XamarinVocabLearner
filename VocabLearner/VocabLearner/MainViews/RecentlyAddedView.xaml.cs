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
        public ObservableCollection<string> Items = new ObservableCollection<string>();

        RecentlyAdded recentController = new RecentlyAdded();
        public RecentlyAddedView()
        {
            InitializeComponent();


         //   Word newWord = new Word
         //   {
         //       sourceWord = "test",
         //       translatedWord = "test"
         //   };

         //\   recentController.AddNewWord(newWord);

            List<Word> recentWords = recentController.GetAllWords();



            //           Items = new ObservableCollection<Word>
            //           {
            //               "Item 1",
            //               "Item 2",
            //               "Item 3",
            //               "Item 4",
            //               "Item 5"
            //         
           // Items = new ObservableCollection<string>();
            if (recentWords != null)
            {
                for (int i = 0; i < recentWords.Count; i++)
                {
                  //  Console.WriteLine(recentWords[i].sourceWord + " - " + recentWords[i].translatedWord);
                    Items.Add(recentWords[i].sourceWord + " - " + recentWords[i].translatedWord);
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
