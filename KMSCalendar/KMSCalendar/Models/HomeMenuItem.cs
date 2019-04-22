namespace KMSCalendar.Models
{
    public class HomeMenuItem
    {
        //* Public Properties
        public MenuItemType Id { get; set; }
 
        public string Title { get; set; }
    }

    public enum MenuItemType
    {
        About,
        Calendar,
        Login,
        SignUp,
        Settings
    }
}