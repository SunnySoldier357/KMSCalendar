using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace KMSCalendar.ViewModels
{
    public class LogInViewModel : INotifyPropertyChanged
    {
        //* Private Properties
        private string email;
        private string password;
        private string loginValidationMessage;

        //* Constructor
        public LogInViewModel() => logInAttempts = 0;

        //* Public Properties
        public int logInAttempts;

        public string Email
        {
            get => email;
            set
            {
                modifyProperty(ref value, ref email, nameof(Email));
            }
        }

        public string Password
        {
            get => password;
            set
            {
                modifyProperty(ref value, ref password, nameof(Password));
            }
        }

        public string LoginValidationMessage
        {
            get => loginValidationMessage;
            set
            {
                modifyProperty(ref value, ref loginValidationMessage, nameof(LoginValidationMessage));
            }
        }


        //* Events
        public event PropertyChangedEventHandler PropertyChanged;

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
