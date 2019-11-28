using KMSCalendar.Models.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace KMSCalendar.Services
{
    public static class ClassManager
    {
        public static int PutInClass(Class c)
        {
            string sql = @"INSERT INTO dbo.Classes (Name, TeacherId) VALUES (@Name, @SchoolId)";
            
            return SqlAccess.SaveData(sql, c);
        }
    }
}
