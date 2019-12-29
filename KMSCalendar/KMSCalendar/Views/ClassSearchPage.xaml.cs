using System;
using System.Threading.Tasks;

using KMSCalendar.Extensions;
using KMSCalendar.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KMSCalendar.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ClassSearchPage : ContentPage
	{
		//* Public Properties
		public ThemeImageSource SearchImageSource { get; }

		//* Constructors
		public ClassSearchPage()
		{
			InitializeComponent();

			SearchImageSource = new ThemeImageSource("search_blue.png", "search_white.png",
				nameof(ClassSearchPage));

			BindingContext = new ClassSearchViewModel();
			SearchIconImage.BindingContext = this;

			MessagingCenter.Subscribe<ClassSearchViewModel>(this, "GoToCalendarAsync",
				async sender => await GoToCalendarAsync());
		}

		//* Public Methods
		public async Task GoToCalendarAsync() =>
			await Navigation.PopModalAsync();

		//* Event Handlers
		private void ClassesListView_ItemSelected(object sender, SelectedItemChangedEventArgs e) =>
			ClassesListView.SelectedItem = null;

		private void GoToNewClassButton_Clicked(object sender, EventArgs e) =>
			Navigation.PushModalAsync(new NewClassPage(this));
	}
}