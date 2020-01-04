using KMSCalendar.Models;
using KMSCalendar.Models.Data;
using KMSCalendar.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KMSCalendar.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AssignmentDetailPage : ContentPage
	{
		//* Constructors
		public AssignmentDetailPage(Assignment assignment)
		{
			InitializeComponent();

			BindingContext = new AssignmentDetailViewModel(assignment);

			MessagingCenter.Subscribe<AssignmentDetailViewModel>(this,
				MessagingEvent.GoBackToAssignmentsPage,
				async sender => await Navigation.PushAsync(new AssignmentsPage()));
		}
	}
}