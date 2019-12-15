using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;

using Xamarin.Forms;

using KMSCalendar.Models.Data;
using KMSCalendar.Services.Data;
using KMSCalendar.Views;

namespace KMSCalendar.ViewModels
{
    public class AssignmentsViewModel : BaseViewModel
    {
        //* Private Properties
        private App app = (Application.Current as App);

        /// <summary>A List of all the Assignments to display.</summary>
        private List<Assignment> assignments;
        private List<Assignment> filteredAssignments;

        //* Public Properties
        public bool ShowCalendarDays => Settings.ShowCalendarDays;

        public DateTime DateChoosen { get; set; }

        public ICommand FilterAssignmentsCommand { get; set; }
        public ICommand LoadAssignmentsCommand { get; set; }

        /// <summary>
        /// A filtered set of all the Assignments that are only for the
        /// current day selected
        /// </summary>
        public List<Assignment> FilteredAssignments
        {
            get => filteredAssignments;
            set => setProperty(ref filteredAssignments, value);
        }

        //* Constructors
        public AssignmentsViewModel()
        {
            Title = "Assignments Calendar";
            DateChoosen = DateTime.Today;

            app.PullEnrolledClasses();

            assignments = new List<Assignment>();
            FilteredAssignments = new List<Assignment>();

            LoadAssignmentsCommand = new Command(() =>
                ExecuteLoadAssignmentsCommand());

            FilterAssignmentsCommand = new Command<DateTime>(selectedDate =>
                ExecuteFilterAssignmentsCommand(selectedDate));

            MessagingCenter.Subscribe<NewAssignmentPage, Assignment>(this,
                "AddAssignment", (page, a) =>
            {
                assignments.Add(a);
                a.UserId = app.SignedInUser.Id;
                a.SetClassId();
                a.SetPeriod();
                AssignmentManager.PutInAssignment(a);

                ExecuteFilterAssignmentsCommand(DateChoosen);
            });

            // This is so that when the class search page closes,
            // the assignment page will update it's assignment list
            MessagingCenter.Subscribe<ClassSearchPage>(this, "LoadAssignments",
                (sender) => ExecuteLoadAssignmentsCommand());

            LoadAssignmentsCommand.Execute(null);

            Settings.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(Settings.ShowCalendarDays))
                    OnNotifyPropertyChanged(nameof(ShowCalendarDays));
            };
        }

        //* Public Methods
        public void ExecuteFilterAssignmentsCommand(DateTime date)
        {
            var result =
                from assignment in assignments.AsParallel()
                where assignment.DueDate.Date.Equals(date.Date)
                orderby assignment.Name
                select assignment;

            FilteredAssignments = result.ToList();
        }

        /// <summary>
        /// Loads Assignments from the db.
        /// </summary>
        public void ExecuteLoadAssignmentsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                // Loads assignments from the db for each class that the user is in.
                var userAssignments = new List<Assignment>();
                if (app.SignedInUser.EnrolledClasses != null)
                {
                    foreach (Class @class in app.SignedInUser.EnrolledClasses)
                    {
                        @class.Assignments = AssignmentManager.LoadAssignments(@class);
                        foreach (Assignment assignment in @class.Assignments)
                            assignment.Class = @class;

                        userAssignments.AddRange(@class.Assignments);
                    }
                }
                assignments = userAssignments;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }

            ExecuteFilterAssignmentsCommand(DateChoosen);
        }
    }
}