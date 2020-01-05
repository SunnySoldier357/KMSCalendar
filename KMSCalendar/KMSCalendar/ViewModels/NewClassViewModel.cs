using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

using KMSCalendar.Extensions;
using KMSCalendar.Models;
using KMSCalendar.Models.Data;
using KMSCalendar.Services.Data;

using Xamarin.Forms;

namespace KMSCalendar.ViewModels
{
	public class NewClassViewModel : BaseViewModel
	{
		//* Private Properties
		private List<Teacher> teachers;
		private List<Teacher> filteredTeachers;

		private string className;
		private string period;
		private string teacherName;

		//* Public Properties
		public ICommand AddClassCommand { get; }
		public ICommand FilterTeachersCommand { get; }
		public ICommand GoBackCommand { get; }

		public List<Teacher> FilteredTeachers
		{
			get => filteredTeachers;
			set => setProperty(ref filteredTeachers, value);
		}

		public string ClassName
		{
			get => className;
			set => setProperty(ref className, value);
		}
		public string Period
		{
			get => period;
			set => setProperty(ref period, value);
		}
		public string TeacherName
		{
			get => teacherName;
			set => setProperty(ref teacherName, value);
		}

		public ThemeImageSource SearchImageSource { get; }

		//* Constructors
		public NewClassViewModel()
		{
			AddClassCommand = new Command<object>(selectedTeacher =>
				addClass(selectedTeacher));
			FilterTeachersCommand = new Command<string>(searchInput =>
				filterTeachers(searchInput));
			GoBackCommand = new Command(() =>
				MessagingCenter.Send(this, MessagingEvent.GoBack));

			SearchImageSource = new ThemeImageSource("search_blue.png", "search_white.png",
				"Shared");

			App.PullSchoolName();
			loadTeachers();
		}

		//* Private Methods
		private void addClass(object selectedTeacher)
		{
			if (int.TryParse(Period, out _))
			{
				// Add Teacher if none selected
				if (!(string.IsNullOrEmpty(TeacherName) || string.IsNullOrEmpty(ClassName)))
				{
					var teacher = addTeacher();
					addClassToDb(teacher.Id, teacher.SchoolId);
				}
				// Otherwise if a teacher is selected
				else if (selectedTeacher != null && !string.IsNullOrEmpty(ClassName))
				{
					var teacher = selectedTeacher as Teacher;
					addClassToDb(teacher.Id, teacher.SchoolId);
				}
			}
		}

		private void addClassToDb(Guid teacherId, Guid schoolId)
		{
			var @class = new Class
			{
				Id = Guid.NewGuid(),
				Name = ClassName,
				Period = int.Parse(Period),
				TeacherId = teacherId,
				UserId = App.SignedInUser.Id,
				SchoolId = schoolId
			};

			// Adds class and new period to the DB
			DataOperation.ConnectToBackend(ClassManager.AddClass, @class);

			MessagingCenter.Send(this, "LoadClasses");
			MessagingCenter.Send(this, MessagingEvent.GoBack);
		}

		private Teacher addTeacher()
		{
			var teacher = new Teacher
			{
				Id = Guid.NewGuid(),
				Name = TeacherName,
				SchoolId = App.SignedInUser.SchoolId
			};

			DataOperation.ConnectToBackend(TeacherManager.AddTeacher, teacher);

			return teacher;
		}

		private void filterTeachers(string searchInput)
		{
			if (string.IsNullOrWhiteSpace(searchInput))
				FilteredTeachers = new List<Teacher>(teachers);
			else
			{
				var result =
					from teacher in teachers.AsParallel()
					where teacher.Name.ToLower().Contains(searchInput.ToLower())
					select teacher;

				FilteredTeachers = result.ToList();
			}
		}

		private void loadTeachers()
		{
			var teacherList = DataOperation.ConnectToBackend(TeacherManager.LoadAllTeachers,
				App.SignedInUser.SchoolId) ?? new List<Teacher>();

			foreach (var teacher in teacherList)
				teacher.SchoolName = App.SchoolName;

			teachers = teacherList;
			FilteredTeachers = teachers;
		}
	}
}