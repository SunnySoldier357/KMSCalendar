using Xamarin.Forms;

namespace KMSCalendar.Models.Navigation
{
    public class HomeMenuItem
    {
        //* Public Properties

        /// <summary>
        /// The image source of the icon for each navigation item.
        /// </summary>
        public ImageSource Source { get; set; }

        /// <summary>
        /// The page to open when this Menu Item is clicked.
        /// </summary>
        public MenuItemType Id { get; set; }

        /// <summary>
        /// The Title that is shown in the Hamburger Menu.
        /// </summary>
        public string Title { get; set; }
    }
}