using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace KMSCalendar.ViewModels
{
    public class SignUpViewModel : INotifyPropertyChanged
    {
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
