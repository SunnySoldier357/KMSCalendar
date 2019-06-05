using KMSCalendar.Models.Data;
using System;
using System.Collections.Generic;
using System.Text;

using KMSCalendar.Services.Data;

namespace KMSCalendar.ViewModels
{
    public class NewClassViewModel : BaseViewModel
    {
        private List<Teacher> teachers;

        public List<Teacher> Teachers
        {
            get => teachers;
            set => setProperty(ref teachers, value);
        }

        public NewClassViewModel()
        {
            Teachers = new List<Teacher>();
        }
    }
}
