using System.Collections.Generic;

namespace KMSCalendar.MobileAppService.Models
{
    public interface IAssignmentRepository
    {
        //* Methods
        Assignment Get(string id);
        IEnumerable<Assignment> GetAll();
        void Add(Assignment assignment);
        Assignment Remove(string key);
        void Update(Assignment assignment);
    }
}