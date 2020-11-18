using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
namespace VocabLearner
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FirstPage : ContentPage
    {

        byte[] profilePicData;
        public FirstPage()
        {
            InitializeComponent();

            username.Text = "Welcome " + App.loggedInUser; //set user logged in details
            profilePicData = App.profilePic; //get profile pic as bytes from app
             
            profilePic.Source = ImageSource.FromStream(() => new MemoryStream(profilePicData)); //set profile pic
        }


    }
}