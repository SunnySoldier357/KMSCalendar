using KMSCalendar.ViewModels;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KMSCalendar.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignUpPage : ContentPage
    {
        //* Private Properties
        //private SignUpViewModel viewModel;
        private LogInViewModel viewModel;

        //* Constructors
        public SignUpPage()
        {
            InitializeComponent();

            //viewModel = new SignUpViewModel();
            viewModel = new LogInViewModel();
            BindingContext = viewModel;
        }

        //* Private Methods

        private bool checkIfPasswordsMatch()
        {
            string password = viewModel.Password;
            string confirmPassword = viewModel.ConfirmPassword;

            if (password.Length > 0 && confirmPassword.Length > 0 && (password == confirmPassword))
            {
                clearValidation();
                return true;
            }
            
            viewModel.LoginValidationMessage = "Passwords do not match";
            return false;
        }

        private bool checkPasswordLength()
        {
            if(viewModel.Password.Length < 8)
            {
                viewModel.LoginValidationMessage = "Password not long enough";
                return false;
            }
            if(viewModel.Password.Length < 64)
            {
                clearValidation();
                return true;
            }

            viewModel.LoginValidationMessage = "Password too long";
            return false;
        }

        private bool checkEmailLength()
        {
            if (viewModel.Email.Length < 5)
            {
                viewModel.LoginValidationMessage = "Email length too short";
                return false;
            }
            if(viewModel.Email.Length < 254)
            {
                clearValidation();
                return true;
            }

            viewModel.LoginValidationMessage = "Email length too long";
            return false;
        }

        private bool checkEmailCharacters()
        {
            if(viewModel.Email.Contains(" "))
            {
                viewModel.LoginValidationMessage = "no spaces in email please";
                return false;
            }
            if(viewModel.Email.Contains("@"))
            {
                clearValidation();
                return true;
            }

            viewModel.LoginValidationMessage = "Please use valid email";
            return false;
        }

        private bool checkUsernameLength()
        {
            if (viewModel.Username.Length < 2)
            {
                viewModel.LoginValidationMessage = "Username length too short";
                return false;
            }
            if (viewModel.Username.Length < 64)
            {
                clearValidation();
                return true;
            }

            viewModel.LoginValidationMessage = "Username length too long";
            return false;
        }

        private void clearValidation() =>
            viewModel.LoginValidationMessage = "";

        //* Event Handlers
        private void AuthenticateSignUpButton_Clicked(object sender, EventArgs e)
        {
            if (checkIfPasswordsMatch() && 
                checkPasswordLength() &&
                checkEmailLength() &&
                checkUsernameLength()
                && checkEmailCharacters())
            {
                //success!
                string userEmail = viewModel.Email;
                string userPassword = viewModel.Password;

                viewModel.LoginValidationMessage = string.Format("Email: {0} Password: {1}", userEmail, userPassword);
                // TODO: KENNY Do something with the sign up info.
            }
        }

        private async void AlreadyUserButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LoginPage(), true); //true makes it animated
        }


    }
}