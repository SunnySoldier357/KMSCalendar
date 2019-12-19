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
        private bool emailVisibility;
        private bool verificationVisibility;
        private bool newPasswordVisibility;

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
        public bool VerificationVisibility
        {
            get => verificationVisibility;
            set => setProperty(ref verificationVisibility, value);
        }
        public bool EmailVisibility
        {
            get => emailVisibility;
            set => setProperty(ref emailVisibility, value);
        }
        public bool NewPasswordVisibility
        {
            get => newPasswordVisibility;
            set => setProperty(ref newPasswordVisibility, value);
        }

        public ICommand AuthenticateEmailCommand { get; }
        public ICommand GoBackCommand { get; }

        public ForgotPasswordViewModel()
        {
            EmailVisibility = true;
            VerificationVisibility = false;
            AuthenticateEmailCommand = new Command(() => AuthenticateEmail());
            GoBackCommand = new Command(() => (Application.Current as App).MainPage.Navigation.PopModalAsync());
        }
        
        private void AuthenticateEmail()
        {
            if (EmailVisibility)
            {
                EmailVisibility = false;
                VerificationVisibility = true;
            }

            else if (VerificationVisibility)
            {
                VerificationVisibility = false;
                NewPasswordVisibility = true;
            }

            else if (NewPasswordVisibility)
            {
                NewPasswordVisibility = false;
                EmailVisibility = true;
            }
        }
    }
}
