﻿using System.Collections.Generic;
using System.Linq;

using Xamarin.Forms;

using KMSCalendar.Models.Data;
using KMSCalendar.Services.Data;
using System.Threading.Tasks;
using KMSCalendar.Views;

namespace KMSCalendar.ViewModels
{
    public class ClassSearchViewModel : BaseViewModel
    {
        //* Private Properties
        private Class selectedClass;
        private List<Class> classes;
        private List<Class> filteredClasses;
        //private List<Class> uniqueClasses;    //This used to be used to filter classes with the search function

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

            MessagingCenter.Subscribe<NewClassPage>(this, "LoadClasses", (sender) =>         //This is so that when the new class page closes,
            {                                                                                       // the class list will update
                LoadClassesAsync();
            });

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
            FilteredClasses = classes;
        }

        //* Public Methods
        public void FilterClasses(string userInput)
        {
            if (string.IsNullOrWhiteSpace(userInput))
                FilteredClasses = new List<Class>(classes);
            else
            {
                var result =
                    from _class in classes.AsParallel()
                    where _class.Name.ToLower().Contains(userInput.ToLower())
                    select _class;

                FilteredClasses = result.ToList();
            }
        }

        public void LoadPeriods()
        {
            int classId = SelectedClass.Id;
            Periods = Services.PeriodManager.LoadPeriods(classId);      //sets the period list to all of the periods in the selected class from the db.
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

        public void AddNewPeriod(int newPeriod)
        {
            selectedClass.Period = newPeriod;
            Services.PeriodManager.PutInClassPeriod(selectedClass);
        }
    }
}