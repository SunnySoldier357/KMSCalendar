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

        //* Event Handlers
        public void OnNotifyPropertyChanged(string property) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));

        //* Private Methods
        private void modifyProperty<T>(ref T value, ref T privateProperty, string nameOfProperty)
        {
            if (!value.Equals(privateProperty))
            {
                privateProperty = value;
                OnNotifyPropertyChanged(nameOfProperty);
            }
        }
    }
}