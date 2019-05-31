using System.ComponentModel;

namespace KMSCalendar.ViewModels
{
    public class LogInViewModel : INotifyPropertyChanged
    {
        
        //* Private Properties
        private string username;
        private string email;
        private string loginValidationMessage;
        private string password;
        private string confirmPassword;

        //* Public Properties
        public int LogInAttempts;

        public string Username
        {
            get => username;
            set => modifyProperty(ref value, ref username, nameof(Username));
        }
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

        //* Events
        public event PropertyChangedEventHandler PropertyChanged;

        //* Constructor
        public LogInViewModel()
        {
            LogInAttempts = 0;
            Username = "";
            Email = "";
            LoginValidationMessage = "";
            Password = "";
            ConfirmPassword = "";
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