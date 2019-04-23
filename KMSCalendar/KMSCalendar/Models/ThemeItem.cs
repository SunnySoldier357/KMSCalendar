namespace KMSCalendar.Models
{
    /// <summary>
    /// Class used to encapsulate Picker Items in the SettingsPage
    /// </summary>
    public class ThemeItem
    {
        //* Public Properties

        /// <summary>
        /// The Name of the current Theme option to be shown.
        /// </summary>
        public string Name;

        /// <summary>
        /// The Theme associated with the current option.
        /// </summary>
        public Theme Theme;

        //* Overridden Methods
        public override string ToString() => Name;
    }
}