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
        private SignUpViewModel viewModel;

        //* Constructors
        public SignUpPage()
        {
            InitializeComponent();

            viewModel = new SignUpViewModel();
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


        //* Event Handlers
        private void AuthenticateSignUpButton_Clicked(object sender, EventArgs e)
        {
            if (viewModel.Password == viewModel.ConfirmPassword)
            {
                string userEmail = viewModel.Email;
                string userPassword = viewModel.Password;

                viewModel.SignUpValidationMessage = string.Format("Email: {0}, Password: {1}", userEmail, userPassword);
                //System.Diagnostics.Debug.WriteLine(string.Format("Email: {0}, Password: {1}", userEmail, userPassword));
                // TODO: Do something with the sign up info.
            }

        }

        private void AlreadyUserButton_Clicked(object sender, EventArgs e)
        {
            // TODO: Redirect to log in page.
        }


    }
}