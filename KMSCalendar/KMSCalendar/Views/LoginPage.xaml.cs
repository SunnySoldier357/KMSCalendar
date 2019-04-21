using KMSCalendar.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KMSCalendar.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginPage : ContentPage
	{
        public LogInViewModel logInDataBind;

        public LoginPage()
        {
            InitializeComponent();
            logInDataBind = new LogInViewModel();
            setBindings();
        }

        private void setBindings()
        {
            EmailEntry.BindingContext = logInDataBind;
            PasswordEntry.BindingContext = logInDataBind;
            LoginValidationLabel.BindingContext = logInDataBind;
        }



        //check if correct login and password
        private void AuthenticateLoginButton_Clicked(object sender, System.EventArgs e)
        {
            logInDataBind.LoginValidationMessage = "Login totally invalid.";
        }

        //go to sign up page.
        private void NewUserButton_Clicked(object sender, System.EventArgs e)
        {
            logInDataBind.LoginValidationMessage = "Sign up page under development.";
        }

        private void ForgotPasswordButton_Clicked(object sender, System.EventArgs e)
        {
            logInDataBind.LoginValidationMessage = "You can't forget your password if you don't have an account.";
        }
    }
}