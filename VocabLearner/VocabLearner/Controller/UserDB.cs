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
            return allPersons.Where(a => a.username == username).FirstOrDefault();
        }

        public async Task<bool> Signup(string username, string password, string profilePic)
        {



            //CALL LOGIN, IF NOT NULL -- NEED TO THROW ERROR (USERNAME TAKEN)



            await firebase
              .Child("Users")
              .PostAsync(new User() { username = username, password = password, profilePic = profilePic });


            return true;
        }

    }
}
