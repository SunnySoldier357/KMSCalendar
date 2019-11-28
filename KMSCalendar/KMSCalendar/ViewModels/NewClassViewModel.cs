using System.Collections.Generic;

using Xamarin.Forms;

using KMSCalendar.Models.Data;
using KMSCalendar.Services.Data;

namespace KMSCalendar.ViewModels
{
    public class NewClassViewModel : BaseViewModel
    {
        //* Private Properties
        private int period;

        private List<Teacher> teachers;

        private string className;
        private string searchTerm;
        private string teacherName;

        private Teacher selectedTeacher;

        //* Public Properties
        public int Period
        {
            get => period;
            set => setProperty(ref period, value);
        }

        public List<Teacher> Teachers
        {
            get => teachers;
            set => setProperty(ref teachers, value);
        }

        public string ClassName
        {
            get => className;
            set => setProperty(ref className, value);
        }
        public string SearchTerm
        {
            get => searchTerm;
            set => setProperty(ref searchTerm, value);
        }
        public string TeacherName
        {
            get => teacherName;
            set => setProperty(ref teacherName, value);
        }
        public Teacher SelectedTeacher
        {
            get => selectedTeacher;
            set => setProperty(ref selectedTeacher, value);
        }
        
        //* Constructors
        public NewClassViewModel(string schoolName)
        {
            var dataStore = DependencyService.Get<IDataStore<Teacher>>();

            Period = 1;
            Teachers = KMSCalendar.Services.TeacherManager.LoadAllTeachers();
            SearchTerm = "";
        }
    }
}