using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Org.BouncyCastle.Asn1.Esf;
using SQLite;
using VocabLearner.Controller;

namespace VocabLearner
{

    public struct Question // for the game tab - a question contains the source text, and 4 answers
    {
        public String text;
        public List<Answer> answers;
    };

    public struct Answer //for the game tab - an answer contains the translation text, and boolean being either correct or false
    {
        public String text;
        public bool correct;
    };


    public class LocalDB
    {
        readonly SQLiteAsyncConnection _database;

        public LocalDB(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Word>().Wait();
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
            RecentlyAdded.DeleteWord(words);
            return _database.DeleteAsync(words);
        }







        public async Task<List<Question>> GetTenWordsAsync() //Returns 10 words for the game
        {
            List<Question> questions = new List<Question>(); //questions is a list of Question struct (see top of file)
            Question questionToAdd = new Question();

            List<Word> allWords = await _database.Table<Word>().ToListAsync(); //get all the words from the database storage

            //variable definitions for each questioN:
            String question = ""; //a question will have
            Answer answer; //1 right answer
            Answer wrongAnswer1; //and 3 wrong answers
            Answer wrongAnswer2;
            Answer wrongAnswer3;

            if (allWords.Count < 10) //if there is less than 10 words in the database
            {
                return null; //return null -> game needs 10 words to play
            }



            allWords = allWords.OrderBy(x => Guid.NewGuid()).ToList(); //this "randomizes" the list by a newly created GUID and turns the result into a new list
            //not perfect for randomization as it relises on the machine and time, not any randomising element, but it works for what we need: https://improveandrepeat.com/2018/08/a-simple-way-to-shuffle-your-lists-in-c/

            int random1; //defines integer variables to be random
            int random2; //answers from other words
            int random3; //in the allWords list

            for(int i = 0; i<10; i++) //loop through 10 times
            {
                Random r = new Random();
                random1 = i;
                random2 = i;
                random3 = i;
                while (random1 == i || random2 == i || random3 == i || random1 == random2 || random1 == random3 || random2 == random3)
                { //randomly generates until all items are random and different - wrng answers also cannot be i as that will be the correct answer
                    random1 = r.Next(0, allWords.Count - 1);
                    random2 = r.Next(0, allWords.Count - 1); //between a range of 0 -> number of words - 1
                    random3 = r.Next(0, allWords.Count - 1);
                }

                //sets the wrongAnswer variables to be one of the incorrect answers (taken from allWords list)
                //sets the correct boolean variable of each to be "false" so the games know that these are incorrect answers
                wrongAnswer1 = new Answer
                {
                    text = allWords[random1].translatedWord,
                    correct = false
                };
                wrongAnswer2 = new Answer
                {
                    text = allWords[random2].translatedWord,
                    correct = false
                };
                wrongAnswer3 = new Answer
                {
                    text = allWords[random3].translatedWord,
                    correct = false
                };

                question = allWords[i].sourceWord; //sets the question
                answer = new Answer()
                {
                    text = allWords[i].translatedWord,
                    correct = true
                }; // sets the real answer (true for correct)

                List<Answer> answersToAdd = new List<Answer>();

                //populate question object 
                questionToAdd.text = question;
                answersToAdd.Add(answer);
                answersToAdd.Add(wrongAnswer1);
                answersToAdd.Add(wrongAnswer2);
                answersToAdd.Add(wrongAnswer3);
                questionToAdd.answers = answersToAdd;


                //append to questions array here
                questions.Add(questionToAdd);

            }




            return questions;
        }
    }
}