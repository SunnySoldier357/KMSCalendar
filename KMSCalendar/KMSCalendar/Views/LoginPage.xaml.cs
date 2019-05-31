using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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

        //* Private Methods
        private bool checkEmail()
        {
            if(viewModel.Email.Length < 5)
            {
                viewModel.LoginValidationMessage = "Email too short";
                return false;
            }
            if(viewModel.Email.Length < 254)
            {
                clearValidation();
                return true;
            }

            viewModel.LoginValidationMessage = "Email too long";
            return false;
        }
        private bool checkPassword()
        {
            if(viewModel.Password.Length < 8)
            {
                viewModel.LoginValidationMessage = "Password too short";
            }
            if(viewModel.Password.Length < 64)
            {
                clearValidation();
                return true;
            }

            viewModel.LoginValidationMessage = "Password too long";
            return false;
        }
        private void clearValidation()
        {
            viewModel.LoginValidationMessage = "";
        }



        //* Event Handlers

        /// <summary> Check if correct login and password </summary>
        private void AuthenticateLoginButton_Clicked(object sender, System.EventArgs e)
        {
            if(checkEmail() && checkPassword())
            {
                string email = viewModel.Email;
                string password = viewModel.Password;

                viewModel.LoginValidationMessage = "login sucess";
                //TODO: Authenticate with backend
            }
        }


        private void ForgotPasswordButton_Clicked(object sender, System.EventArgs e) =>
            viewModel.LoginValidationMessage = "You can't forget your password if you don't have an account.";

        /// <summary>
        /// Go to sign up page
        /// </summary>
        private async void NewUserButton_Clicked(object sender, System.EventArgs e) =>
            await Navigation.PushAsync(new SignUpPage(), true); //true makes it animated
    }
}