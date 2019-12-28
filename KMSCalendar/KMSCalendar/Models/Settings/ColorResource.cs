using Autofac;

using Xamarin.Forms;

namespace KMSCalendar.Models.Settings
{
    public class ColorResource
    {
        //* Public Properties
        public Color? CurrentThemeColor
        {
            get
            {
                if (DarkThemeColor == null && LightThemeColor == null)
                    return null;

                var userSettings = AppContainer.Container.Resolve<UserSettings>();

                string hex;
                if (DarkThemeColor == null)
                    hex = LightThemeColor;
                else
                    hex = userSettings.Theme == Theme.Light ? LightThemeColor : DarkThemeColor;

                return Color.FromHex(hex);
            }
        }

        public string DarkThemeColor { get; private set; }
        public string LightThemeColor { get; private set; }
        public string Name { get; private set; }

        //* Overriden MEthods
        public override bool Equals(object obj)
        {
            if (obj is ColorResource other)
                return DarkThemeColor == other.DarkThemeColor &&
                    LightThemeColor == other.LightThemeColor &&
                    Name == other.Name;
            else
                return false;
        }
    }
}