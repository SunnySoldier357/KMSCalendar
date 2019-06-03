using ModelValidation;

namespace KMSCalendar.ViewModels
{
    public class LogInViewModel : BaseViewModel
    {
        //* Private Properties
        private int logInAttempts;

        private string email;
        private string loginValidationMessage;
        private string password;

        //* Public Properties
        public int LogInAttempts
        {
            get => logInAttempts;
            set => setProperty(ref logInAttempts, value);
        }

        [ContainsCharacter('@')]
        [DoesNotContainCharacter(' ')]
        [MinimumLength(5)]
        [MaximumLength(254)]
        public string Email
        {
            get => email;
            set => setProperty(ref email, value);
        }
        public string LoginValidationMessage
        {
            get
            {
                if (Errors != null && Errors.Count > 0)
                    return Errors[0];

                return loginValidationMessage;
            }
            set => setProperty(ref loginValidationMessage, value);
        }
        [MinimumLength(8)]
        [MaximumLength(64)]
        public string Password
        {
            get => password;
            set => setProperty(ref password, value);
        }

        //* Constructor
        public LogInViewModel()
        {
            Email = string.Empty;
            Password = string.Empty;

            PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(Errors))
                    OnNotifyPropertyChanged(nameof(LoginValidationMessage));
            };
        }
    }
}