using System.Collections.Generic;
using System.Linq;

using Xamarin.Forms;

using KMSCalendar.Models.Data;
using KMSCalendar.Services.Data;
using System.Threading.Tasks;

namespace KMSCalendar.ViewModels
{
    public class ClassSearchViewModel : BaseViewModel
    {
        //* Private Properties
        private IDataStore<Class> dataStore;
        private Class selectedClass;

        private List<Class> classes;
        private List<Class> filteredClasses;
        private List<Class> uniqueClasses;

        private List<int> periods;

        private App app = (Application.Current as App);

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

            //dataStore = DependencyService.Get<IDataStore<Class>>();

            LoadClassesAsync();
        }

        /// <summary>
        /// Loads a list of classes from the DB so the user can add one to their account.
        /// </summary>
        /// <returns></returns>
        public void LoadClassesAsync()
        {
            // TODO: MATEO get this to work so a user doesn't have duplicate classes.

            List<Class> classList = Services.ClassManager.LoadClasses(app.GetSchoolId());

            foreach (Class c in classList)
            {
                string name = Services.TeacherManager.LoadTeacherNameFromId(c.TeacherId);
                c.Teacher = new Teacher() { Name = name };
            }

            classes = classList;
            filteredClasses = classes;

            //LEGACY CODE:
            //
            //var classes =
            //    from _class in await dataStore.GetItemsAsync(true)
            //    let userClasses =
            //        from userClass in (Application.Current as App).SignedInUser.EnrolledClasses
            //        select userClass.Id
            //    where !userClasses.Contains(_class.Id)
            //    select _class;
            //
            //var classes = await dataStore.GetItemsAsync(true);
            //
            //this.classes = classes.ToList();
            //uniqueClasses = this.classes.Distinct(new DuplicateClassNameComparer()).ToList();
            //FilteredClasses = new List<Class>(uniqueClasses);
            //filteredClasses = this.classes;
            //Periods = new List<int>();
        }

        //* Public Methods
        public void FilterClasses(string userInput)
        {
            if (string.IsNullOrWhiteSpace(userInput))
                FilteredClasses = new List<Class>(uniqueClasses);
            else
            {
                var result =
                    from _class in uniqueClasses.AsParallel()
                    where _class.Name.ToLower().Contains(userInput.ToLower())
                    select _class;

                FilteredClasses = result.ToList();
            }
        }

        public void LoadPeriods()
        {
            //TODO: Mateo TODAY figure out the periods db.
            // THIS IS CAUSING THE CURRENT ERROR WHEN THE USER CLICKS ON A CLASS
            var periods =
                from _class in classes.AsParallel()
                where _class.Name == SelectedClass.Name &&
                    _class.Teacher.Equals(SelectedClass.Teacher)
                select _class.Period;

            Periods = periods.ToList();
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

        //private class DuplicateClassNameComparer : EqualityComparer<Class>
        //{
        //    //* Overridden Methods
        //    //public override bool Equals(Class x, Class y) => 
        //    //    x.Name == y.Name && x.Teacher?.Name == y.Teacher?.Name;

        //    //public override int GetHashCode(Class obj) => 
        //    //    $"{obj.Name} ({obj.Teacher?.Name ?? ""})".GetHashCode();
        //}
    }
}