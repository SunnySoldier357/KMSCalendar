using System.ComponentModel;

namespace KMSCalendar.ViewModels
{
    public class LogInViewModel : INotifyPropertyChanged
    {
        //* Private Properties
        private string email;
        private string loginValidationMessage;
        private string password;
        private string confirmPassword;
        private string userName;

        //* Public Properties
        public int LogInAttempts { get; set; }

        public string Email
        {
            get => email;
            set => modifyProperty(ref value, ref email, nameof(Email));
        }
        public string LoginValidationMessage
        {
            get => loginValidationMessage;
            set => modifyProperty(ref value, ref loginValidationMessage,
                nameof(LoginValidationMessage));
        }
        public string Password
        {
            get => password;
            set => modifyProperty(ref value, ref password, nameof(Password));
        }
        public string ConfirmPassword
        {
            get => confirmPassword;
            set => modifyProperty(ref value, ref confirmPassword, nameof(ConfirmPassword));
        }
        public string UserName
        {
            get => userName;
            set => modifyProperty(ref value, ref userName, nameof(UserName));
        }

        //* Events
        public event PropertyChangedEventHandler PropertyChanged;

        //* Constructor
        public LogInViewModel()
        {
            Email = string.Empty;
            LoginValidationMessage = string.Empty;
            Password = string.Empty;
            ConfirmPassword = string.Empty;
            UserName = string.Empty;
        }

        //* Public Methods
        public bool IsLoginModelValid()
        {
            LoginValidationMessage = string.Empty;

            bool result = validateProperty(ref email, nameof(Email), 5, 254);

            if (result && Email.Contains(" "))
            {
                LoginValidationMessage = "No spaces in email please";
                result = false;
            }
            if (result && !Email.Contains("@"))
            {
                LoginValidationMessage = "Please use valid email";
                result = false;
            }

            if (result)
                result = validateProperty(ref password, nameof(Password), 8, 64);

            return result;
        }

        public bool IsSignUpModelValid()
        {
            LoginValidationMessage = string.Empty;

            bool result = validateProperty(ref userName, nameof(UserName), 2, 64);

            if (result)
                result = IsLoginModelValid();

            if (result && (Password != ConfirmPassword))
            {
                LoginValidationMessage = "Passwords do not match";
                result = false;
            }

            return result;
        }

        //* Event Handlers
        public void OnNotifyPropertyChanged(string property) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));

        //* Private Methods
        private bool invalidModel(string validationMessage)
        {
            LoginValidationMessage = validationMessage;
            return false;
        }

        private void modifyProperty<T>(ref T value, ref T privateProperty, string nameOfProperty)
        {
            if (!value.Equals(privateProperty))
            {
                privateProperty = value;
                OnNotifyPropertyChanged(nameOfProperty);
            }
        }

        private bool validateProperty(ref string property, string nameOfProperty,
            int minimum, int maximum)
        {
            if (string.IsNullOrWhiteSpace(property))
                return invalidModel($"No {nameOfProperty}");
            else if (property.Length < minimum)
                return invalidModel($"{nameOfProperty} Too Short");
            else if (property.Length > maximum)
                return invalidModel($"{nameOfProperty} Too Long");

            return true;
        }
    }
}