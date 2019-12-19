using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace KMSCalendar.ViewModels
{
    class ForgotPasswordViewModel : BaseViewModel
    {
        private string email;
        private string validationMessage;

        public string Email
        {
            get => email;
            set => setProperty(ref email, value);
        }
        public string ValidationMessage
        {
            get => validationMessage;
            set => setProperty(ref validationMessage, value);
        }

        public ICommand AuthenticateEmailCommand { get; }
        public ICommand GoBackCommand { get; }

        public ForgotPasswordViewModel()
        {
            AuthenticateEmailCommand = new Command(() => AuthenticateEmail());
            GoBackCommand = new Command(() => (Application.Current as App).MainPage.Navigation.PopModalAsync());
        }

        private void AuthenticateEmail()
        {

        }
    }
}
