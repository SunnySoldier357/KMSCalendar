using System.Collections.Generic;

using KMSCalendar.Models.Data;
using KMSCalendar.Services.Data;

using Xamarin.Forms;

namespace KMSCalendar.ViewModels
{
	public class NewClassViewModel : BaseViewModel
	{
		//* Private Properties
		private string period;

		private List<Teacher> teachers;

		private string className;
		private string searchTerm;
		private string teacherName;

		private Teacher selectedTeacher;

		//* Public Properties
		public string Period
		{
			get => period;
			set => setProperty(ref period, value);
		}

		public List<Teacher> Teachers
		{
			get => teachers;
			set
			{
				foreach (Teacher t in value)
					t.SchoolName = App.SchoolName;
				setProperty(ref teachers, value);
			}
		}

		public string ClassName
		{
			get => className;
			set => setProperty(ref className, value);
		}
		public string SearchTerm
		{
			get => searchTerm;
			set => setProperty(ref searchTerm, value);
		}
		public string TeacherName
		{
			get => teacherName;
			set => setProperty(ref teacherName, value);
		}
		public Teacher SelectedTeacher
		{
			get => selectedTeacher;
			set => setProperty(ref selectedTeacher, value);
		}

		//* Constructors
		public NewClassViewModel()
		{
			App.PullSchoolName();
			Teachers = DataOperation.ConnectToBackend(TeacherManager.LoadAllTeachers, App.SignedInUser.SchoolId);
			SearchTerm = "";
		}
	}
}