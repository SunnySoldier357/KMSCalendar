using ModelValidation;

namespace KMSCalendar.ViewModels
{
    public class LogInViewModel : ValidatableObject
    {
        //* Private Properties
        private string email;
        private string loginValidationMessage;
        private string password;

        //* Public Properties
        public int LogInAttempts { get; set; }

        [ContainsCharacter('@')]
        [DoesNotContainCharacter(' ')]
        [MinimumLength(5)]
        [MaximumLength(254)]
        public string Email
        {
            get => email;
            set => modifyProperty(ref value, ref email, nameof(Email));
        }
        public string LoginValidationMessage
        {
            get
            {
                if (Errors != null && Errors.Count > 0)
                    return Errors[0];

                return loginValidationMessage;  
            }
            set => modifyProperty(ref value, ref loginValidationMessage,
                nameof(LoginValidationMessage));
        }
        [MinimumLength(8)]
        [MaximumLength(64)]
        public string Password
        {
            get => password;
            set => modifyProperty(ref value, ref password, nameof(Password));
        }

        //* Constructor
        public LogInViewModel()
        {
            Email = string.Empty;
            LoginValidationMessage = string.Empty;
            Password = string.Empty;

            PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(Errors))
                    OnNotifyPropertyChanged(nameof(LoginValidationMessage));
            };
        }
    }
}