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
            //TODO: Mateo TODAY check class model to have school Id
            string sql = @"INSERT INTO dbo.Classes (Period, Name, TeacherId, UserId, SchoolId) VALUES (@Period, @Name, @TeacherId, @UserId, @SchoolId)";
            
            return SqlAccess.SaveData(sql, c);
        }
    }
}
