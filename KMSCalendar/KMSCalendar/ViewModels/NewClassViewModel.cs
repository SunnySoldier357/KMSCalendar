using System.Collections.Generic;

using KMSCalendar.Models.Data;

namespace KMSCalendar.ViewModels
{
    public class NewClassViewModel : BaseViewModel
    {
        //* Private Properties
        private List<Teacher> teachers;

        //* Public Properties
        public List<Teacher> Teachers
        {
            get => teachers;
            set => setProperty(ref teachers, value);
        }

        //* Constructors
        public NewClassViewModel() => Teachers = new List<Teacher>();
    }
}