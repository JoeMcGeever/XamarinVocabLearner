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
            wordPair.sourceWord = "BIND TEST";
            wordPair.translatedWord = "BIND TEST";
            TransWordBind = wordPair;

            BindingContext = wordPair;
            InitializeComponent();
        }

        private void Recently_Added_OnClicked(object sender, EventArgs e)
        {

        }
        private void Delete_OnClicked(object sender, EventArgs e)
        {

        }
        private void Edit_OnClicked(object sender, EventArgs e)
        {

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


            return newWord;
        }


        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            System.Console.WriteLine("Method is called");
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}