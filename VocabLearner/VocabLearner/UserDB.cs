using System;
using MySql.Data.MySqlClient;
//using MySqlConnector;


namespace VocabLearner
{
    public class UserDB
    {

        public static string connectionString = "Server=127.0.0.1; Port=3306; Database=vocab_learner; Uid=root; Pwd=;";

        public bool connect()
        {

            return true;


            MySqlConnection dbConnection = new MySqlConnection(connectionString);
            try
            {
                dbConnection.Open();


            }catch(Exception e)
            {
                throw e;
                return false;
            }

            return true;
    
        }







    }
}
