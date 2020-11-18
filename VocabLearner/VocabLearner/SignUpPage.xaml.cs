using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Essentials;
using Xamarin.Forms.Xaml;
using Plugin.Media;

namespace VocabLearner
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignUpPage : ContentPage
    {

        UserDB usersDatabase = new UserDB();

        public SignUpPage()
        {
            InitializeComponent();

            //Initialise the takePhoto and pickPhoto buttons

            takePhoto.Clicked += async (sender, args) =>
            {

                if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                {
                    await DisplayAlert("No Camera", ":( No camera avaialble.", "OK");
                    return;
                }

                var photo = await Plugin.Media.CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions() { });

                if (photo != null)
                    image.Source = ImageSource.FromStream(() => { return photo.GetStream();
                    });
            };


            pickPhoto.Clicked += async (sender, args) =>
                {
                    if (!CrossMedia.Current.IsPickPhotoSupported)
                    {
                        await DisplayAlert("Photos Not Supported", ":( Permission not granted to photos.", "OK");
                        return;
                    }
                    var file = await Plugin.Media.CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
                    {
                        PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium
                    });


                    if (file == null)
                        return;

                    image.Source = ImageSource.FromStream(() =>
                    {
                        var stream = file.GetStream();
                        file.Dispose();
                        return stream;
                    });
                };

        }


        private void LogIn_OnClicked(object sender, EventArgs e)
        {
            //await Navigation.PushAsync(new SignUpPage());
            Application.Current.MainPage.Navigation.PopAsync();
        }



        private async void SignUp_OnClicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(usernameEntry.Text) //if the username entry field
            && !string.IsNullOrWhiteSpace(passwordEntry.Text)) //or password ones are not empty
            {


                if(await usersDatabase.Signup(usernameEntry.Text, passwordEntry.Text, "pig.png")) //save to firebase database here
                {
                    await Application.Current.MainPage.Navigation.PopAsync(); //pop this view off the current navigation stack
                }
                else
                {
                    await DisplayAlert("Error!", "Username already taken", "Ok");
                }
            }
            else
            {
                await DisplayAlert("Error!", "Please enter all of your details", "Ok");
            }

        }



    }


 }