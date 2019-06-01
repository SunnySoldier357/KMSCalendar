using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

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

        //* Public Properties

        /// <summary>
        /// The Command that loads Assignments from the IDataStore
        /// </summary>
        public Command LoadAssignmentsCommand { get; set; }

        /// <summary>A List of all the Assignments to display</summary>
        public ObservableCollection<Assignment> Assignments { get; set; }
        /// <summary>
        /// A filtered set of all the Assignments that are only for the
        /// current day selected
        /// </summary>
        public ObservableCollection<Assignment> FilteredAssignments { get; set; }

        //* Constructors
        public AssignmentViewModel()
        {
            dataStore = DependencyService.Get<IDataStore<Assignment>>();

            Title = "Assignments Calendar";

            Assignments = new ObservableCollection<Assignment>();
            FilteredAssignments = new ObservableCollection<Assignment>();

            LoadAssignmentsCommand = new Command(async () =>
                await ExecuteLoadAssignmentsCommand());

            MessagingCenter.Subscribe<NewAssignmentPage, Assignment>(this,
                "AddAssignment", async (page, a) =>
            {
                Assignment newItem = a as Assignment;
                Assignments.Add(newItem);
                await dataStore.AddItemAsync(newItem);
            });
        }

        //* Public Methods

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
                Assignments.Clear();
                var assignments = await dataStore.GetItemsAsync(true);
                foreach (Assignment assignment in assignments)
                    Assignments.Add(assignment);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }

            FilterAssignments(DateTime.Today);
        }

        public void FilterAssignments(DateTime date)
        {
            FilteredAssignments.Clear();

            var result =
                from a in Assignments.AsParallel()
                where a.DueDate.Day == date.Day &&
                    a.DueDate.Month == date.Month &&
                    a.DueDate.Year == date.Year
                orderby a.Name, a.Description
                select a;

            foreach (Assignment assignment in result)
                FilteredAssignments.Add(assignment);
        }
    }
}