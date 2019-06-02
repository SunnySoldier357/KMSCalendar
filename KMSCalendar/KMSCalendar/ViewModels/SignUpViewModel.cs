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
            set => modifyProperty(ref value, ref confirmPassword, nameof(ConfirmPassword));
        }
        [MinimumLength(2)]
        [MaximumLength(64)]
        public string UserName
        {
            get => userName;
            set => modifyProperty(ref value, ref userName, nameof(UserName));
        }

        //* Constructors
        public SignUpViewModel() : base()
        {
            ConfirmPassword = string.Empty;
            UserName = string.Empty;
        }
    }
}