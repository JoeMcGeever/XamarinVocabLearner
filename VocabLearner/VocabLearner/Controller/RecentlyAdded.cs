using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xamarin.Forms;
namespace VocabLearner.Controller
{
    public class RecentlyAdded
    {

        public List<Word> recentWords = new List<Word>();

        public List<Word> GetAllWords()
        {

            return recentWords; //returns the recent word list
        }

        public void AddNewWord(Word wordToBeAdded)
        {
            recentWords.Add(wordToBeAdded);
            //logic to delete first entry if too long
            if(recentWords.Count > 10)
            {
                recentWords.RemoveAt(0);
            }
        }

    }
}
