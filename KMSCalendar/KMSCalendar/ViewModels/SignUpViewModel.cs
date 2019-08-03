﻿using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

using ModelValidation;

using Xamarin.Forms;

using KMSCalendar.Models;
using KMSCalendar.Models.Data;
using KMSCalendar.Services.Data;
using KMSCalendar.Views;

namespace KMSCalendar.ViewModels
{
    public class SignUpViewModel : LogInViewModel
    {
        //* Private Properties
        private string confirmPassword;
        private string userName;

        //* Public Properties
        public ICommand AlreadyUserCommand { get; set; }
        public new ICommand AuthenticateUserCommand { get; set; }

        [PropertyValueMatch(nameof(Password), 
            ErrorMessage = "The Passwords do not match!")]
        public string ConfirmPassword
        {
            get => confirmPassword;
            set => setProperty(ref confirmPassword, value);
        }
        [MinimumLength(2)]
        [MaximumLength(64)]
        public string UserName
        {
            get => userName;
            set => setProperty(ref userName, value);
        }

        //* Constructors
        public SignUpViewModel() : base()
        {
            Title = "Sign Up";

            ConfirmPassword = string.Empty;
            UserName = string.Empty;

            AlreadyUserCommand = new Command(() => ExecuteAlreadyUserCommand());
            AuthenticateUserCommand = new Command(async () => await ExecuteAuthenticateUserCommand());
        }

        //* Public Methods
        public void ExecuteAlreadyUserCommand() =>
            (Application.Current as App).MainPage = new LoginPage();

        public new async Task ExecuteAuthenticateUserCommand()
        {
            if (Validate())
            {
                var dataStore = DependencyService.Get<IDataStore<User>>();
                var users = await dataStore.GetItemsAsync(true);

                if (users.SingleOrDefault(u => u.Email == Email) != null)
                    Errors.Add("User already exists! Please log in instead.");
                else
                {
                    string hashedPassword = PasswordHasher.HashPassword(Password);

                    User user = new User
                    {
                        Email = Email,
                        UserName = UserName,
                        Password = hashedPassword
                    };


                    User signedInUser = await dataStore.AddItemAsync(user);

                    App app = Application.Current as App;

                    app.SignedInUser = signedInUser;
                    Settings.DefaultInstance.SignedInUserId = signedInUser.Id;

                    app.MainPage = new MainPage();
                }
            }
        }
    }
}