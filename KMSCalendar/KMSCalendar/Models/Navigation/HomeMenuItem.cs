using KMSCalendar.Extensions;

namespace KMSCalendar.Models.Navigation
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

        /// <summary>
        /// The image source of the icon for each navigation item.
        /// </summary>
        public ThemeImageSource Source { get; set; }
    }
}