using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace KMSCalendar.ViewModels
{
    public class SignUpViewModel : INotifyPropertyChanged
    {
        //* Private Properties
        private string email;
        private string password;
        private string confirmPassword;

        //* Public Properties
        public string Email
        {
            get => email;
            set => modifyProperty(ref value, ref email, nameof(Email));
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


        public event PropertyChangedEventHandler PropertyChanged;
    }
}
