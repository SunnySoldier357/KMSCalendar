using System;

using KMSCalendar.Models;
using KMSCalendar.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KMSCalendar.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NewAssignmentPage : ContentPage
	{
		//* Contructors
		public NewAssignmentPage() : this(DateTime.Today) { }

		/// <summary>
		/// This constructor is used with the date selected on the calendar.
		/// </summary>
		/// <param name="dateSelected"></param>
		public NewAssignmentPage(DateTime dateSelected)
		{
			InitializeComponent();

			BindingContext = new NewAssignmentViewModel(dateSelected);

			MessagingCenter.Subscribe<NewAssignmentViewModel>(this, MessagingEvent.GoBack,
				async sender => await Navigation.PopModalAsync());

			MessagingCenter.Subscribe<NewAssignmentViewModel>(this, MessagingEvent.GoToClassSearchPage,
				async sender => await Navigation.PushModalAsync(new ClassSearchPage()));
		}
	}
}