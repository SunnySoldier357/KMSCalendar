﻿using System;
using System.Collections.Generic;

namespace KMSCalendar.Models.Data
{
	public class User : TableData
	{
		//* Public Properties
		public Guid SchoolId { get; set; }

		public List<Class> EnrolledClasses { get; set; }

		public string Email { get; set; }
		public string Password { get; set; }
		public string UserName { get; set; }
	}
}