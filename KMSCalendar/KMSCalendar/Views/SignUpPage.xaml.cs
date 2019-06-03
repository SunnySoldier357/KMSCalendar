using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using KMSCalendar.Models;
using KMSCalendar.Models.Entities;
using KMSCalendar.Services;
using KMSCalendar.ViewModels;

namespace KMSCalendar.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignUpPage : ContentPage
    {
        //* Private Properties
        private SignUpViewModel viewModel;

        //* Constructors
        public SignUpPage()
        {
            InitializeComponent();

            viewModel = new SignUpViewModel();
            BindingContext = viewModel;
        }

        //* Event Handlers
        private async void AuthenticateSignUpButton_Clicked(object sender, EventArgs e)
        {
            if (viewModel.Validate())
            {
                string hashedPassword = PasswordHasher.HashPassword(viewModel.Password);

                User user = new User
                {
                    Email = viewModel.Email,
                    UserName = viewModel.UserName,
                    Password = viewModel.Password
                };

                var dataStore = DependencyService.Get<IDataStore<User>>();

                //* Have DataStore return the Created User
                var signedInUser = await dataStore.AddItemAsync(user);

                App app = Application.Current as App;

                // TODO: Change this
                app.SignedInUser = user;
                Settings.DefaultInstance.SignedInUserId = user.Id;

                app.MainPage = new MainPage();
            }
        }

        private void AlreadyUserButton_Clicked(object sender, EventArgs e) => 
            (Application.Current as App).MainPage = new LoginPage();
    }
}