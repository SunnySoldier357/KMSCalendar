using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

using Xamarin.Forms;

using KMSCalendar.Models.Entities;
using KMSCalendar.Services;
using KMSCalendar.Views;

namespace KMSCalendar.ViewModels
{
    public class AssignmentViewModel : BaseViewModel
    {
        //* Private Properties
        private IDataStore<Assignment> dataStore;

        /// <summary>A List of all the Assignments to display</summary>
        private List<Assignment> assignments;
        private List<Assignment> filteredAssignments;

        //* Public Properties

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
        public AssignmentViewModel()
        {
            Title = "Assignments Calendar";

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
                await dataStore.AddItemAsync(newItem);
            });

            LoadAssignmentsCommand.Execute(null);
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
                var userAssignments =
                    from assignment in await dataStore.GetItemsAsync(true)
                    let userClasses = 
                        from _class in (Application.Current as App).SignedInUser.EnrolledClasses
                        select _class.Id
                    where userClasses.Contains(assignment.Class.Id)
                    select assignment;

                assignments = userAssignments.ToList();
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