using KMSCalendar.Models.Data;
using KMSCalendar.Services.Data;
using ModelValidation;
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
        private bool emailVisibility;

        private string code;
        private bool verificationVisibility;

        private string password;
        private string confirmPassword;
        private bool newPasswordVisibility;

        private bool successVisibility;
        private bool goBackVisibility;
        private string validationMessage;


        [ContainsCharacter('@')]
        [DoesNotContainCharacter(' ')]
        [MinimumLength(5)]
        [MaximumLength(254)]
        public string Email
        {
            get => email;
            set => setProperty(ref email, value);
        }
        public bool EmailVisibility
        {
            get => emailVisibility;
            set => setProperty(ref emailVisibility, value);
        }


        public string Code
        {
            get => code;
            set => setProperty(ref code, value);
        }
        public bool VerificationVisibility
        {
            get => verificationVisibility;
            set => setProperty(ref verificationVisibility, value);
        }


        [MinimumLength(8)]
        [MaximumLength(64)]
        public string Password
        {
            get => password;
            set => setProperty(ref password, value);
        }
        [PropertyValueMatch(nameof(Password),
            ErrorMessage = "The Passwords do not match!")]
        public string ConfirmPassword
        {
            get => confirmPassword;
            set => setProperty(ref confirmPassword, value);
        }
        public bool NewPasswordVisibility
        {
            get => newPasswordVisibility;
            set => setProperty(ref newPasswordVisibility, value);
        }


        public bool SuccessVisibility
        {
            get => successVisibility;
            set => setProperty(ref successVisibility, value);
        }
        public bool GoBackVisibility
        {
            get => goBackVisibility;
            set => setProperty(ref goBackVisibility, value);
        }
        public string ValidationMessage
        {
            get
            {
                if (Errors != null && Errors.Count > 0)
                    return Errors[0];

                return validationMessage;
            }
            set => setProperty(ref validationMessage, value);
        }

        public ICommand AuthenticateEmailCommand { get; }
        public ICommand AuthenticateCodeCommand { get; }
        public ICommand AuthenticateNewPasswordCommand { get; }
        public ICommand GoBackCommand { get; }

        public ForgotPasswordViewModel()
        {
            EmailVisibility = true;
            VerificationVisibility = false;

            PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(Errors))
                    OnNotifyPropertyChanged(nameof(ValidationMessage));
            };

            AuthenticateEmailCommand = new Command(() => AuthenticateEmail());
            AuthenticateCodeCommand = new Command(() => AuthenticateCode());
            AuthenticateNewPasswordCommand = new Command(() => AuthenticateNewPassword());
            GoBackCommand = new Command(() => (Application.Current as App).MainPage.Navigation.PopModalAsync());
        }
        
        private void AuthenticateEmail()
        {
            if (Validate())
            {
                User user = UserManager.LoadUserFromEmail(Email);
                if (user != null)
                {
                    //TODO: SUNNY send the token via email
                    SwapViews();

                }
                else
                    ValidationMessage = "Please enter the email address for your account.";
            }

        }

        private void AuthenticateCode()
        {
            if (Code.Length == 6)       //update to the correct length of the code
            {

                //TODO: SUNNY verify if the code the user entered is correct
                bool verified = true;
                if (verified)
                    SwapViews();
                else
                    ValidationMessage = "Please enter a valid email address.";
            }
            else
                ValidationMessage = "Please enter a valid email address.";
        }

        private void AuthenticateNewPassword()
        {
            if (Validate())      
            {
                //TODO: SUNNY update the db with the new password

                SwapViews();
            }
        }

        private void SwapViews()
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
                SuccessVisibility = true;
                GoBackVisibility = false;
            }

            ValidationMessage = "";
        }
    }
}
