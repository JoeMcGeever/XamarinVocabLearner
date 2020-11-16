using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Collections;
using Newtonsoft.Json; //to deserialize JSOn

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using VocabLearner.Controller;
using Google.Cloud.Translation.V2;

namespace VocabLearner.MainViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TranslatePage : ContentPage
    { 

        public string translatedText;
        private List<(string, string)> languageList;
        public string sourceText;
       
        public TranslatePage()
        {
            languageList = languageCodes.GetSupportedLanguages(); //fetch the supported languages
            InitializeComponent();

        }

        private async void Go_clicked(object sender, EventArgs e)
        {
            if ((!string.IsNullOrWhiteSpace(searchWord.Text))
                && (!string.IsNullOrWhiteSpace(language.Text)))
            {

                var targetLangCode = language.Text;


                foreach(var item in languageList) //convert language to language code!
                {
                    if(item.Item1.ToLower()==language.Text.ToLower())
                    {
                        targetLangCode = item.Item2;
                    }
                }

                sourceText = searchWord.Text;
                translatedText = TranslateText(searchWord.Text, targetLangCode);
                translatedWord.Text = translatedText;
            }

        }

        private async void Add_OnClicked(object sender, EventArgs e)
        {

            //when the add button is clicked, the entries are checked whether they are empty first, then if it already exists on the system, and then adds the word to the dB and to recently added file
            if (string.IsNullOrWhiteSpace(sourceText) //if the source word entry field
            | string.IsNullOrWhiteSpace(translatedWord.Text)) //or translated ones are empty
            {
                await DisplayAlert("Error!", "Please enter all of your details", "Ok");
                return;
            }


            var wordPair = await App.Database.GetOneWord(sourceText);

            if (wordPair != null)//if already in the system
            {
                await DisplayAlert("Error!", "The word " + sourceText + " already exists on the system (see edit tab for more details)", "Ok");
            }
            else
            {
                await App.Database.SaveWordsAsync(new Word //add the new word to the dB
                {
                    sourceWord = sourceText,
                    translatedWord = translatedWord.Text
                });

                await DisplayAlert("Success!", "Word Added", "Ok");
                

            }

            sourceText = null;

        }



        public string TranslateText(string input, string langCode) //returns the translation
        {

            // Set the language from/to in the url (or pass it into this function)
            string url = String.Format
            ("https://translation.googleapis.com/language/translate/v2?key=AIzaSyDI_mVKWuLP5ml6RkkplWne1iDRr_KC2GA&q={0}&target={1}",
             Uri.EscapeUriString(input), langCode);

            HttpClient httpClient = new HttpClient();
            var result = httpClient.GetStringAsync(url).Result;

            // Get all json data
            dynamic jsonData = JsonConvert.DeserializeObject(result); //convert into json readable obj

            string translation;

            try
            {
                translation = jsonData.data.translations[0].translatedText;
            }
            catch
            {
                translation = "Translation not found";
            }

            // Return translation
            return translation;
        }



    }
}