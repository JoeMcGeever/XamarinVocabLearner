using System;
using System.IO;

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

        async void OnImageButtonClicked(object sender, EventArgs e)
        {
            await profilePic.RotateTo(360, 2000);
            profilePic.Rotation = 0;
        }

    }
}