using System;
using System.Collections.Generic;
using System.Text;
using Google.Cloud.Translation.V2;

namespace VocabLearner.Controller
{
    class TranslateAPI
    {


        public static string translate()
        {
            var client = TranslationClient.Create();
            var text = "This will be sent text in the future";
            var response = client.TranslateText(text, LanguageCodes.German, LanguageCodes.English);
            //maybe do - if target language is in languagecodes.<AVAILABLE OPTIONS HERE>
            //perhaps there is a comprehensive list somewhere
            //or a call -- better an array in language codes
            Console.WriteLine(response.TranslatedText);
            Console.WriteLine(response);
            return response.TranslatedText;
        }
    }
}
