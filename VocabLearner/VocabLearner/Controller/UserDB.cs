using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Firebase.Database;
using Firebase.Database.Query;
using VocabLearner.Controller;

namespace VocabLearner
{
    public class UserDB
    {
        FirebaseClient firebase = new FirebaseClient("https://vocab-learner-28fe7.firebaseio.com/");


        public async Task<List<User>> GetAllPersons()  
        {  
  
            return (await firebase  
              .Child("Users")  
              .OnceAsync<User>()).Select(item => new User  
              {  
                  username = item.Object.username,  
                  password = item.Object.password,
                  profilePic = item.Object.profilePic
              }).ToList();  
        }  


        public async Task<User> Login(string username, string password)
        {
            var allPersons = await GetAllPersons();
            await firebase
              .Child("Users")
              .OnceAsync<User>();


            return allPersons.Where(a => (a.username == username) && (a.password == password)).FirstOrDefault(); //return the user which matches the password as well as the username
        }

        public async Task<bool> Signup(string username, string password, string profilePic)
        {

            //if user already exists
            var allPersons = await GetAllPersons();
            await firebase
              .Child("Users")
              .OnceAsync<User>();

            if(allPersons.Where(a => a.username == username).FirstOrDefault() != null) //if a person has the same username
            {
                return false;
            }


            await firebase
              .Child("Users")
              .PostAsync(new User() { username = username, password = password, profilePic = profilePic }); //post request to dB server
            //creates a new user


            return true;
        }

    }
}
