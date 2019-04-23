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
            setBindings();
        }

        //* Private Methods
        private void setBindings()
        {
            EmailEntry.BindingContext = viewModel;
            PasswordEntry.BindingContext = viewModel;
            ConfirmPasswordEntry.BindingContext = viewModel;
            SignUpValidationLabel.BindingContext = viewModel;
        }

        private bool checkIfPasswordsMatch()
        {
            string password = viewModel.Password;
            string confirmPassword = viewModel.ConfirmPassword;

            if (password.Length > 0 && confirmPassword.Length > 0 && (password == confirmPassword))
            {
                viewModel.LoginValidationMessage = "";
                return true;
            }

            viewModel.LoginValidationMessage = "Passwords do not match";
            return false;
        }


        //* Event Handlers
        private void AuthenticateSignUpButton_Clicked(object sender, EventArgs e)
        {
            if (checkIfPasswordsMatch())
            {
                string userEmail = viewModel.Email;
                string userPassword = viewModel.Password;

                viewModel.LoginValidationMessage = string.Format("Email: {0} Password: {1}", userEmail, userPassword);
                // TODO: Do something with the sign up info.
            }

        }

        private void AlreadyUserButton_Clicked(object sender, EventArgs e)
        {
            // TODO: Redirect to log in page.
        }


    }
}