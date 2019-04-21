namespace KMSCalendar.Models
{
    public class ThemeItem
    {
        //* Public Properties
        public string Name;

        public Theme Theme;

        //* Overridden Methods
        public override string ToString() => Name;
    }
}