using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VocabLearner.Games
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class QuestionPage : ContentPage
    {
        int currentRightNumber; //holds the number which is the correct answer
        int score = 0;
        string currentRightAnswer; //holds the correct answer for the current question
        int currentQuestion = 0;
        List<Question> questionList;
        private List<Answer> currentAnswers;
        bool canPressNext = false; //user can only select next after an answer is pressed

        public QuestionPage(List<Question> listOfQuestions)
        {
            questionList = listOfQuestions; //set the class variable questionList to be the 10 questions taken from prior view

            InitializeComponent();

            setQuestions(); //set the view by calling setQuestions
           
        }

        public void setQuestions() //prepares the view according to currentQuestion
        {
            //set view in accordance with first question

            currentAnswers = questionList[currentQuestion].answers;

            currentAnswers = currentAnswers.OrderBy(x => Guid.NewGuid()).ToList(); //this "randomizes" the list by a newly created GUID and turns the result into a new list -- see localDB for more details


            for(int i = 0; i < currentAnswers.Count; i++) //loops through the new current answers
            {
                if (currentAnswers[i].correct == true) //the one with the boolean value "true" (so is the right answer)
                {
                    currentRightAnswer = currentAnswers[i].text; // set the current right answer to be the text of this instance
                    currentRightNumber = i + 1; //set the right answer to be where the answer is in the array (+1 because arrays count from 0)
                    break;
                }
            }

            questionNum.Text = "Question " + (currentQuestion + 1); //set the question number text
            question.Text = questionList[currentQuestion].text; //set the question
            option1.Text = currentAnswers[0].text; //set the questions
            option2.Text = currentAnswers[1].text; 
            option3.Text = currentAnswers[2].text;
            option4.Text = currentAnswers[3].text;
            answer.FadeTo(0, 2000); //fade out answer text
            //answer.Text = ""; //get rid of the answer.text as it is not needed
        }

        private async void one_OnClicked(object sender, EventArgs e)
        {
            if (canPressNext == true) //if the user has already selected an answer
            {
                return;
            }
            if (currentRightNumber == 1)
            {
                //correct
                score += 1; //increment score
                answer.Text = "Correct!";
            }
            else
            {
                answer.Text = "Incorrect! The correct answer was: " + currentRightAnswer;
            }
            answer.FadeTo(1, 2000); //fade answer text in


            canPressNext = true;

        }

        private async void two_OnClicked(object sender, EventArgs e)
        {
            if (canPressNext == true) //if the user has already selected an answer
            {
                return;
            }
            if (currentRightNumber == 2)
            {
                //correct
                score += 1; //increment score
                answer.Text = "Correct!";
            }
            else
            {
                answer.Text = "Incorrect! The correct answer was: " + currentRightAnswer;
            }
            answer.FadeTo(1, 2000); //fade answer text in

            canPressNext = true;

        }

        private async void three_OnClicked(object sender, EventArgs e)
        {
            if (canPressNext == true) //if the user has already selected an answer
            {
                return;
            }
            if (currentRightNumber == 3)
            {
                //correct
                score += 1; //increment score
                answer.Text = "Correct!";
            }
            else
            {
                answer.Text = "Incorrect! The correct answer was: " + currentRightAnswer;
            }
            answer.FadeTo(1, 2000); //fade answer text in


            canPressNext = true;

        }

        private async void four_OnClicked(object sender, EventArgs e)
        {
            if(canPressNext == true) //if the user has already selected an answer
            {
                return;
            }
            if (currentRightNumber == 4)
            {
                //correct
                score += 1; //increment score
                answer.Text = "Correct!";
            } else
            {
                answer.Text = "Incorrect! The correct answer was: " + currentRightAnswer;
            }
            answer.FadeTo(1, 2000); //fade answer text in

            canPressNext = true;

        }

        private async void Next_OnClicked(object sender, EventArgs e) //reset view
        {
            if (canPressNext == false) //if the user hasnt answered yet
            {
                return;
            }




            currentQuestion += 1; //append 1 to current question
            if(currentQuestion==10) //when current question gets to 10, then the game is over
            {
                await DisplayAlert("Congratulations!", "You have completed the game and scored: " + score + "/10!", "ok");
                await Application.Current.MainPage.Navigation.PopAsync(); //return to prior page;
                return;
            }

            canPressNext = false; //stop users from being able to press next

            setQuestions(); //refresh board now the current question has incremented

        }
    }
}