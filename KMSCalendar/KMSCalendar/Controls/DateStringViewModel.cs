using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace KMSCalendar.Controls
{
    public class DateStringViewModel : INotifyPropertyChanged
    {
        private string dateString;
        public string DateString
        {
            get => dateString;
            set
            {
                if(value != dateString)
                {
                    dateString = value;
                    OnNotifyPropertyChanged(nameof(DateString));
                }              
            }
        }



        public event PropertyChangedEventHandler PropertyChanged;

        //* Event Handlers
        public void OnNotifyPropertyChanged(string property)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

    }
}
