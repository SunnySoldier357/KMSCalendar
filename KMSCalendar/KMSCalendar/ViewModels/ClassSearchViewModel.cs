using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

using Xamarin.Forms;

using KMSCalendar.Models.Entities;
using KMSCalendar.Services;

namespace KMSCalendar.ViewModels
{
    public class ClassSearchViewModel
    {
        //* Private Properties
        private List<Class> classes;
        private List<Class> uniqueClasses;

        //* Public Properties
        public Class SelectedClass { get; set; }

        public ObservableCollection<Class> FilteredClasses { get; set; }
        public ObservableCollection<int> Periods { get; set; }

        //* Constructors
        public ClassSearchViewModel()
        {
            var dataStore = DependencyService.Get<IDataStore<Class>>();

            var classes =
                from _class in dataStore.GetItemsAsync().Result
                let userClasses =
                    from userClass in (Application.Current as App).SignedInUser.EnrolledClasses
                    select userClass.Id
                where !userClasses.Contains(_class.Id)
                select _class;

            this.classes = classes.ToList();
            uniqueClasses = this.classes.Distinct(new DuplicateClassNameComparer()).ToList();
            FilteredClasses = new ObservableCollection<Class>(uniqueClasses);
            Periods = new ObservableCollection<int>();
        }

        //* Public Methods
        public void FilterClasses(string userInput)
        {
            if (!string.IsNullOrWhiteSpace(userInput))
                FilteredClasses = new ObservableCollection<Class>(uniqueClasses);
            else
            {
                FilteredClasses.Clear();

                var result =
                    from _class in uniqueClasses.AsParallel()
                    where _class.Name.ToLower().Contains(userInput.ToLower())
                    select _class;

                foreach (Class _class in result)
                    FilteredClasses.Add(_class);
            }
        }

        public void LoadPeriods()
        {
            Periods.Clear();

            var periods =
                from _class in classes.AsParallel()
                where _class.Name == SelectedClass.Name &&
                    _class.Teacher.Equals(SelectedClass.Teacher)
                select _class.Period;

            foreach (int period in periods)
                Periods.Add(period);
        }

        private class DuplicateClassNameComparer : EqualityComparer<Class>
        {
            //* Overridden Methods
            public override bool Equals(Class x, Class y) => x.Name == y.Name;

            public override int GetHashCode(Class obj) => obj.Name.GetHashCode();
        }
    }
}