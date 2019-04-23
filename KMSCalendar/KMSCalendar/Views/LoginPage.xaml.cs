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
            setBindings();
        }

        //* Private Methods
        private void setBindings()
        {
            EmailEntry.BindingContext = viewModel;
            PasswordEntry.BindingContext = viewModel;
            LoginValidationLabel.BindingContext = viewModel;
        }

        //* Event Handlers

        /// <summary> Check if correct login and password </summary>
        private void AuthenticateLoginButton_Clicked(object sender, System.EventArgs e) =>
            viewModel.LoginValidationMessage = "Login totally invalid.";

        private void ForgotPasswordButton_Clicked(object sender, System.EventArgs e) =>
            viewModel.LoginValidationMessage = "You can't forget your password if you don't have an account.";

        /// <summary>
        /// Go to sign up page
        /// </summary>
        private void NewUserButton_Clicked(object sender, System.EventArgs e) =>
            viewModel.LoginValidationMessage = "Sign up page under development.";
    }
}