namespace KMSCalendar.UWP
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            InitializeComponent();

            LoadApplication(new KMSCalendar.App());
        }
    }
}