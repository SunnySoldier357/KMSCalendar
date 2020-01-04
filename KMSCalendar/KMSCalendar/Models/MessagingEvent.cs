using System.Runtime.CompilerServices;

namespace KMSCalendar.Models
{
	public static class MessagingEvent
	{
		//* Public Properties
		public static string AddAssignment => autoName();
		public static string CalendarWeekControlItemSelected => autoName();
		public static string ClassesListViewDeselectItem => autoName();
		public static string GoBack => autoName();
		public static string GoBackToAssignmentsPage => autoName();
		public static string GoBackToCalendar => autoName();
		public static string GoToNewClassPage => autoName();

		//* Private Methods
		private static string autoName([CallerMemberName] string propertyName = "") =>
			propertyName;
	}
}