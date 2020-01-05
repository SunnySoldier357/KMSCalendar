using KMSCalendar.Models;
using KMSCalendar.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KMSCalendar.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NewClassPage : ContentPage
	{
		//* Public Properties
		public NewClassViewModel ViewModel;

		//* Constructors
		public NewClassPage()
		{
			InitializeComponent();

			MessagingCenter.Subscribe<NewClassViewModel>(this, MessagingEvent.GoBack,
				async sender => await Navigation.PopModalAsync());
		}
	}
}