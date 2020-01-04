using System;
using System.Threading.Tasks;

using KMSCalendar.Services.Data;

using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KMSCalendar.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NetworkFailPage : ContentPage
	{
		//* Private Properties
		private DataOperation operation;

		//* Constructors
		public NetworkFailPage() : this(null) { }

		public NetworkFailPage(DataOperation operation)
		{
			InitializeComponent();

			this.operation = operation;
		}

		//* Event Handlers
		private async void RetryButton_Clicked(object sender, EventArgs e)
		{
			LoadingAnimation.IsLoading = true;

			await handleConnectionAsync();

			LoadingAnimation.IsLoading = false;
		}

		//* Private Methods
		private async Task handleConnectionAsync()
		{
			if (operation?.TryToGetData() ?? false)
				operation.WaitHandle.Set();

			if (Connectivity.NetworkAccess == NetworkAccess.Internet)
			{
				// Connection to internet is available
				await Navigation.PopModalAsync();
			}
		}
	}
}