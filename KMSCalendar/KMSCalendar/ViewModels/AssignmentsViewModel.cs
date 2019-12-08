using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
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
        private IDataStore<Assignment> dataStore;
        private App app = (Application.Current as App);

        //private AssignmentsPage parentPage;

        /// <summary>A List of all the Assignments to display</summary>
        private List<Assignment> assignments;
        private List<Assignment> filteredAssignments;

        //* Public Properties
        public bool ShowCalendarDays => Settings.ShowCalendarDays;

        public DateTime DateChoosen { get; set; }

        public ICommand FilterAssignmentsCommand { get; set; }
        /// <summary>
        /// The Command that loads Assignments from the IDataStore
        /// </summary>
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

            dataStore = DependencyService.Get<IDataStore<Assignment>>();

            assignments = new List<Assignment>();
            FilteredAssignments = new List<Assignment>();

            LoadAssignmentsCommand = new Command(async () =>
                await ExecuteLoadAssignmentsCommand());

            FilterAssignmentsCommand = new Command<DateTime>(selectedDate =>
                ExecuteFilterAssignmentsCommand(selectedDate));

            MessagingCenter.Subscribe<NewAssignmentPage, Assignment>(this,
                "AddAssignment", async (page, a) =>
            {
                Assignment newItem = a as Assignment;
                assignments.Add(newItem);
                a.UserId = app.SignedInUser.Id;
                a.SetClassId();
                a.SetPeriod();
                Services.AssignmentManager.PutInAssignment(a);
                
                ExecuteFilterAssignmentsCommand(DateChoosen);
            });

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
                orderby assignment.Class.Name, assignment.Name
                select assignment;

            FilteredAssignments = result.ToList();
        }

        /// <summary>
        /// Loads Assignments from the IDataStore.
        /// </summary>
        public async Task ExecuteLoadAssignmentsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                //var userAssignments;    //TODO MATEO TODAY: select assignments from the db!
                string s = "s";
                //LEGACY CODE
                //var userAssignments =
                //    from assignment in await dataStore.GetItemsAsync(true)
                //    let userClasses = 
                //        from _class in (Application.Current as App).SignedInUser.EnrolledClasses
                //        select _class.Id
                //    where userClasses.Contains(assignment.Class.Id)
                //    select assignment;

                //assignments = userAssignments.ToList();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }

            ExecuteFilterAssignmentsCommand(DateTime.Today);
        }
    }
}