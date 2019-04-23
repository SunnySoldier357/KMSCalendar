using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KMSCalendar.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignUpPage : ContentPage
    {
        //* Constructors
        public SignUpPage() =>
            InitializeComponent();

        //* Event Handlers
        private void AlreadyUserButton_Clicked(object sender, EventArgs e)
        {
            // TODO: Do something with the sign up info.
        }

        private void AuthenticateSignUpButton_Clicked(object sender, EventArgs e)
        {
            // TODO: Redirect to log in page.
        }
    }
}