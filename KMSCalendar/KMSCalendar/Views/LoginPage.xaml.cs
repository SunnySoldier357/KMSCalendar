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

        //* Event Handlers

        /// <summary>Check if correct login and password.</summary>
        private void AuthenticateLoginButton_Clicked(object sender, System.EventArgs e)
        {
            if (viewModel.IsLoginModelValid())
            {
                string email = viewModel.Email;
                string password = viewModel.Password;

                viewModel.LoginValidationMessage = "Login success";

                // TODO: Authenticate with backend
            }
        }

        private void ForgotPasswordButton_Clicked(object sender, System.EventArgs e) =>
            viewModel.LoginValidationMessage = "You can't forget your password " +
                "if you don't have an account.";

        /// <summary>Go to sign up page.</summary>
        private async void NewUserButton_Clicked(object sender, System.EventArgs e)
        {
            // true makes it animated
            await Navigation.PushAsync(new SignUpPage(), true);
        }
    }
}