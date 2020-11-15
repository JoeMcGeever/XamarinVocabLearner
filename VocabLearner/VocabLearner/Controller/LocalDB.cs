using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Org.BouncyCastle.Asn1.Esf;
using SQLite;
using VocabLearner.Controller;

namespace VocabLearner
{
    public class LocalDB
    {
        readonly SQLiteAsyncConnection _database;

        public LocalDB(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Word>().Wait();
        }

        public Task<List<Word>> GetWordsAsync() //PROBABLY NOT NEEDED --> ONLY NEED TO GET 1 OR GET 10
        {
            return _database.Table<Word>().ToListAsync();
        }

        public async Task<string> GetOneWord(string sourceWord)
        {
            var listOfObjects = _database.Table<Word>().ToListAsync(); //get the list of words
            foreach(Word item in await listOfObjects) //for each Word item within the list of objects (await for the database function to complete)
            {
                if(sourceWord == item.sourceWord)
                {
                    return item.translatedWord;
                }
            }
            return null;

        }

        public Task<int> SaveWordsAsync(Word words) //saves the words to the database
        {

            RecentlyAdded.AddNewWord(words); //add the new word to the recently added 

            return _database.InsertAsync(words);
        }

        public Task<int> DeleteWordsAsync(Word words) //delete a word pair from the database
        {
            return _database.DeleteAsync(words);
        }
    }
}