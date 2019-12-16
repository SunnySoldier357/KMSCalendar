using System.Collections.Generic;
using System.Linq;

using Xamarin.Forms;

using KMSCalendar.Models.Data;
using KMSCalendar.Services.Data;
using KMSCalendar.Views;

namespace KMSCalendar.ViewModels
{
    public class ClassSearchViewModel : BaseViewModel
    {
        //* Private Properties
        private Class selectedClass;

        private List<Class> classes;
        private List<Class> filteredClasses;

        private List<int> periods;

        private App app => (Application.Current as App);

        //* Public Properties
        public Class SelectedClass
        {
            get => selectedClass;
            set => setProperty(ref selectedClass, value);
        }

        public List<Class> FilteredClasses
        {
            get => filteredClasses;
            set => setProperty(ref filteredClasses, value);
        }
        public List<int> Periods
        {
            get => periods;
            set => setProperty(ref periods, value);
        }

        //* Constructors
        public ClassSearchViewModel()
        {
            Title = "Search For Class";

            // This is so that when the new class page closes,
            // the class list will update
            MessagingCenter.Subscribe<NewClassPage>(this, "LoadClasses",
                (sender) => LoadClassesAsync());

            LoadClassesAsync();
        }

        //* Public Methods
        public bool AddNewPeriod(int newPeriod)
        {
            //checks if the period already exists
            if (periods.Contains(newPeriod))
                return false;

            //else add the period to the db
            selectedClass.Period = newPeriod;
            PeriodManager.PutInClassPeriod(selectedClass);
            return true;
        }

        public void FilterClasses(string userInput)
        {
            if (string.IsNullOrWhiteSpace(userInput))
                FilteredClasses = new List<Class>(classes);
            else
            {
                var result =
                    from @class in classes.AsParallel()
                    where @class.Name.ToLower().Contains(userInput.ToLower())
                    select @class;

                FilteredClasses = result.ToList();
            }
        }

        /// <summary>
        /// Loads a list of classes from the DB so the user can add one to their account.
        /// </summary>
        public void LoadClassesAsync()
        {
            // TODO: MATEO get this to work so a user doesn't have duplicate classes.
            List<Class> classList = ClassManager.LoadClasses(app.SignedInUser.SchoolId);

            foreach (Class @class in classList)
            {
                string name = TeacherManager.LoadTeacherNameFromId(@class.TeacherId);
                @class.Teacher = new Teacher(name);
            }

            classes = classList;
            FilteredClasses = classes;
        }

        public void LoadPeriods()
        {
            int classId = SelectedClass.Id;

            // Sets the period list to all of the periods in the selected class from the db.
            Periods = PeriodManager.LoadPeriods(classId);
        }

        /// <summary>
        /// For some reason I need this method and cannot just add a new int to the periods list
        /// </summary>
        public void LoadPeriods(int newPeriod)
        {
            var periods =
                from _class in classes.AsParallel()
                where _class.Name == SelectedClass.Name &&
                    _class.Teacher.Equals(SelectedClass.Teacher)
                select _class.Period;

            Periods = periods.ToList();
            Periods.Add(newPeriod);
        }
    }
}