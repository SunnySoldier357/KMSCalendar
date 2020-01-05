using KMSCalendar.Models;
using KMSCalendar.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KMSCalendar.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ClassSearchPage : ContentPage
	{
		//* Constructors
		public ClassSearchPage()
		{
			InitializeComponent();

			MessagingCenter.Subscribe<ClassSearchViewModel>(this, MessagingEvent.GoBackToCalendar,
				async sender => await Navigation.PopModalAsync());

			MessagingCenter.Subscribe<ClassSearchViewModel>(this, MessagingEvent.ClassesListViewDeselectItem,
				sender => ClassesListView.SelectedItem = null);

			MessagingCenter.Subscribe<ClassSearchViewModel>(this, MessagingEvent.GoToNewClassPage,
				async sender => await Navigation.PushModalAsync(new NewClassPage()));
		}
	}
}