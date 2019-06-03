using System;

using KMSCalendar.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KMSCalendar.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignUpPage : ContentPage
    {
        //* Private Properties
        //private SignUpViewModel viewModel;
        private SignUpViewModel viewModel;

        //* Constructors
        public SignUpPage()
        {
            InitializeComponent();

            viewModel = new SignUpViewModel();
            BindingContext = viewModel;
        }

        //* Event Handlers
        private void AuthenticateSignUpButton_Clicked(object sender, EventArgs e)
        {
            if (viewModel.Validate())
            {
                // Success!
                string userEmail = viewModel.Email;
                string userPassword = viewModel.Password;

                viewModel.LoginValidationMessage = 
                    string.Format("Email: {0} Password: {1}", userEmail, userPassword);

                // TODO: KENNY Do something with the sign up info.
            }
        }

        private void AlreadyUserButton_Clicked(object sender, EventArgs e) => 
            (Application.Current as App).MainPage = new LoginPage();
    }
}