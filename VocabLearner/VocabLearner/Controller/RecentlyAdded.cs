using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xamarin.Forms;
namespace VocabLearner.Controller
{
    public static class RecentlyAdded //static so recentWords is accesible throughout the application
    {
        public static List<Word> recentWords = new List<Word>();

        public static List<Word> GetAllWords()
        {

            return recentWords; //returns the recent word list
        }

        public static void AddNewWord(Word wordToBeAdded)
        {
            recentWords.Add(wordToBeAdded);
            //logic to delete first entry if too long
            if(recentWords.Count > 10)
            {
                recentWords.RemoveAt(0);
            }
        }

        public static void DeleteWord(Word wordToBeDeleted) //deletes the word from the list
        {
            try
            {
                for (int i = 0; i < recentWords.Count; i++) // Loop through List with for as .Remove() won't work as the objects are different instances (despite holding the same value)
                {
                    Console.WriteLine(recentWords[i]);
                    if (recentWords[i].sourceWord.Equals(wordToBeDeleted.sourceWord))
                    {
                        recentWords.Remove(recentWords[i]); // remove from recently added list
                    }
                }

            } catch
            {
                Console.WriteLine("Word was not in recently added");
            }
        }

    }
}
