using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;

using KMSCalendar.Models;
using KMSCalendar.Views;

namespace KMSCalendar.ViewModels
{
    public class AssignmentViewModel : BaseViewModel
    {
        //* Public Properties
        public Command LoadAssignmentsCommand { get; set; }

        public ObservableCollection<Assignment> Assignments { get; set; }
        public ObservableCollection<Assignment> FilteredAssignments { get; set; }

        //* Constructors
        public AssignmentViewModel()
        {
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
                await DataStore.AddItemAsync(newItem);
            });
        }

        //* Public Methods
        public async Task ExecuteLoadAssignmentsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Assignments.Clear();
                var assignments = await DataStore.GetItemsAsync(true);
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
                select a;

            foreach (Assignment assignment in result)
                FilteredAssignments.Add(assignment);
        }
    }
}