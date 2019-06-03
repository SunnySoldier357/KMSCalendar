using ModelValidation;

namespace KMSCalendar.ViewModels
{
    public class SignUpViewModel : LogInViewModel
    {
        //* Private Properties
        private string confirmPassword;
        private string userName;

        //* Public Properties
        [PropertyValueMatch(nameof(Password))]
        public string ConfirmPassword
        {
            get => confirmPassword;
            set => setProperty(ref confirmPassword, value);
        }
        [MinimumLength(2)]
        [MaximumLength(64)]
        public string UserName
        {
            get => userName;
            set => setProperty(ref userName, value);
        }

        //* Constructors
        public SignUpViewModel() : base()
        {
            ConfirmPassword = string.Empty;
            UserName = string.Empty;
        }
    }
}