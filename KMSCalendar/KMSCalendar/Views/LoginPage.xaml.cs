using System.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using KMSCalendar.Models;
using KMSCalendar.Models.Entities;
using KMSCalendar.Services;
using KMSCalendar.ViewModels;

namespace KMSCalendar.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        //* Private Properties
        private LogInViewModel viewModel;

        //* Constructor
        public LoginPage()
        {
            InitializeComponent();

            viewModel = new LogInViewModel();
            BindingContext = viewModel;
        }

        //* Event Handlers

        /// <summary>Check if correct login and password.</summary>
        private async void AuthenticateLoginButton_Clicked(object sender, System.EventArgs e)
        {
            if (viewModel.Validate())
            {
                string email = viewModel.Email;
                // TODO: Authenticate with backend
                string password = viewModel.Password;
                string databasePassword = "temp"; //TODO: Grab password from database
                if (PasswordHash.ValidatePassword(password, databasePassword))
                {
                    var dataStore = DependencyService.Get<IDataStore<User>>();
                    var users = await dataStore.GetItemsAsync();
                    User signedInUser = users.FirstOrDefault(u => u.Email == email);

                    App app = Application.Current as App;

                    app.SignedInUser = signedInUser;
                    Settings.DefaultInstance.SignedInUserId = signedInUser.Id;

                    app.MainPage = new MainPage();
                }
                else
                {
                    // TODO: Output "Invalid Password"
                }
            }
        }


        private void ForgotPasswordButton_Clicked(object sender, System.EventArgs e) =>
            viewModel.LoginValidationMessage = "You can't forget your password " +
                "if you don't have an account.";

        /// <summary>Go to sign up page.</summary>
        private void NewUserButton_Clicked(object sender, System.EventArgs e) =>
            (Application.Current as App).MainPage = new SignUpPage();
    }
}