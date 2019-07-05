using System.Collections.Generic;

using Xamarin.Forms;

using KMSCalendar.Models.Data;
using KMSCalendar.Services.Data;

namespace KMSCalendar.ViewModels
{
    public class NewClassViewModel : BaseViewModel
    {
        //* Private Properties
        private List<Teacher> teachers;
        private Teacher selectedTeacher;
        private string searchTerm;
        private string className;
        private string teacherName;
        private int period;

        //* Public Properties
        public List<Teacher> Teachers
        {
            get => teachers;
            set => setProperty(ref teachers, value);
        }
        public string SearchTerm
        {
            get => searchTerm;
            set => setProperty(ref searchTerm, value);
        }
        public Teacher SelectedTeacher
        {
            get => selectedTeacher;
            set => setProperty(ref selectedTeacher, value);
        }
        public string ClassName
        {
            get => className;
            set => setProperty(ref className, value);
        }
        public string TeacherName
        {
            get => teacherName;
            set => setProperty(ref teacherName, value);
        }
        public int Period
        {
            get => period;
            set => setProperty(ref period, value);
        }


        //* Constructors
        public NewClassViewModel()
        {
            var dataStore = DependencyService.Get<IDataStore<Teacher>>();
            Teachers = new List<Teacher>(dataStore.GetItemsAsync(false).Result);
            Period = 1;
            SearchTerm = "";
        }
    }
}