namespace KMSCalendar.Models
{
    public class HomeMenuItem
    {
        //* Public Properties

        /// <summary>
        /// The page to open when this Menu Item is clicked.
        /// </summary>
        public MenuItemType Id { get; set; }

        /// <summary>
        /// The Title that is shown in the Hamburger Menu.
        /// </summary>
        public string Title { get; set; }
    }

    public enum MenuItemType
    {
        About,
        Calendar,
        Login,
        Settings,
        SignUp,
    }
}