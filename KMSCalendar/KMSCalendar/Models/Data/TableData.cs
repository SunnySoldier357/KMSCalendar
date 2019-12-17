using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace KMSCalendar.Models.Data
{
    public abstract class TableData
    {
        //* Static Properties
        private static bool seedLoaded = false;

        private static List<Assignment> assignments;
        private static List<Class> classes;
        private static List<Teacher> teachers;
        private static List<User> users;

        private static List<string> assignmentPool = new List<string>
        {
            "Reading",
            "Complete the Assignment",
            "Finish online HW",
            "Take notes",
            "Group Project Due",
            "Comparative Essay"
        };
        private static List<string> classPool = new List<string>
        {
            "Physics",
            "Calculus",
            "Biology",
            "History",
            "American Literature",
            "Chemistry",
            "Economics",
            "Computer Science",
            "Theory of Knowledge"
        };
        private static List<string> teacherPool = new List<string>
        {
            "Smith",
            "Johnson",
            "Williams",
            "Brown",
            "Davis",
            "Miller",
            "Wilson",
            "Moore",
            "Taylor"
        };

        //* Public Properties

        /// <summary>The unique ID of the entity.</summary>
        public string Id { get; set; } = Guid.NewGuid().ToString();

        //* Public Static Methods

        /// <summary>
        /// A method used to seed values if the MockDataStore is being used.
        /// </summary>
        /// <typeparam name="T">A subclass of TableData.</typeparam>
        /// <returns>An IEnumerable of the seeded instances of TableData.</returns>
        public static IEnumerable Seed<T>() where T : TableData
        {
            if (!seedLoaded)
            {
                seedListsInit();
                seedLoaded = true;
            }

            Type temp = typeof(T);

            if (temp.Equals(typeof(Assignment)))
                return assignments;
            else if (temp.Equals(typeof(Class)))
                return classes;
            else if (temp.Equals(typeof(Teacher)))
                return teachers;
            else if (temp.Equals(typeof(User)))
                return users;

            return null;
        }

        //* Private Static Methods
        private static int getAnotherNumber(int min, int max, params int[] nums)
        {
            Random random = new Random();
            int temp = nums[0];

            while (nums.Contains(temp))
                temp = random.Next(min, max);

            return temp;
        }

        private static void seedListsInit()
        {
            Random random = new Random();

            teachers = new List<Teacher>();
            for (int i = 0; i < 3; i++)
            {
                string surname = "";
                switch (random.Next(0, 2))
                {
                    case 0: surname = "Ms. ";
                        break;
                    case 1: surname = "Mrs. ";
                        break;
                    case 2: surname = "Mr. ";
                        break;
                }
    
                teachers.Add(new Teacher
                {
                    Name = surname + teacherPool.ElementAt(i)
                });
            }

            classes = new List<Class>();
            for (int i = 0; i < 3; i++)
            {
                int num = random.Next(1, 7);
                string className = classPool.ElementAt(random.Next(0, classPool.Count));
                Teacher teacher = teachers[i];

                classes.Add(new Class
                {
                    Name = className,
                    Period = num,
                    Teacher = teacher
                });

                int num2 = getAnotherNumber(1, 7, num);

                classes.Add(new Class
                {
                    Name = className,
                    Period = num2,
                    Teacher = teacher
                });

                classes.Add(new Class
                {
                    Name = className,
                    Period = getAnotherNumber(1, 7, num, num2),
                    Teacher = teacher
                });
            }

            int count = 0;
            for (int i = 0; i < 3; i++)
            {
                teachers[i].Classes = new List<Class>
                {
                    classes[count++],
                    classes[count++],
                    classes[count++]
                };
            }

            assignments = new List<Assignment>();
            for (int i = 0; i < 30; i++)
            {
                assignments.Add(new Assignment
                {
                    Name = assignmentPool.ElementAt(random.Next(0, assignmentPool.Count)),
                    Description = $"From pages 1 - {i} (test item)",
                    DueDate = DateTime.Today.AddDays(random.Next(-2, 3)),
                    Class = classes[random.Next(0, classes.Count)]
                });
            }

            var groupedAssignments = assignments.GroupBy(a => a.Class);
            foreach (var group in groupedAssignments)
            {
                Class _class = group.Key;
                _class.Assignments = new List<Assignment>();

                foreach (Assignment assignment in group)
                    _class.Assignments.Add(assignment);
            }

            List<Class> tempClasses = new List<Class>
            {
                classes[0],
                classes[3],
                classes[6]
            };

            string password = PasswordHasher.HashPassword("testPassword");

            users = new List<User>
            {
                new User
                {
                    Id = Guid.NewGuid(),
                    Email = "singh@king.com",
                    UserName = "SinghIsKing",
                    Password = password,
                    EnrolledClasses = tempClasses
                },
                new User
                {
                    Id = Guid.NewGuid(),
                    Email = "test@king.com",
                    UserName = "No Class Test",
                    Password = password,
                    EnrolledClasses = new List<Class>()
                },
                new User
                {
                    Id = Guid.NewGuid(),
                    Email = "mattmorgan6@gmail.com",
                    UserName = "mattmorgan6",
                    Password = password,
                    EnrolledClasses = tempClasses
                }
            };
        }
    }
}