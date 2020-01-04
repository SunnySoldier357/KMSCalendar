using KMSCalendar.Models;
using KMSCalendar.Services.Data;
using KMSCalendar.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KMSCalendar.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NetworkFailPage : ContentPage
	{
		//* Constructors
		public NetworkFailPage() : this(null) { }

		public NetworkFailPage(DataOperation operation)
		{
			InitializeComponent();

			BindingContext = new NetworkFailViewModel(operation);

			MessagingCenter.Subscribe<NetworkFailViewModel>(this, MessagingEvent.GoBack,
				async sender => await Navigation.PopModalAsync());
		}
	}
}