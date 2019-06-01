using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KMSCalendar.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SelectPeriodView : ContentView
	{
        //public event EventHandler DataViewChanged;
        public ClassSearchPage parentPage;

        public SelectPeriodView()
        {
            InitializeComponent();
        }

        private void BackButton_Clicked(object sender, EventArgs e)
        {
            parentPage.swap();
            // Raise the event
            //DataViewChanged?.Invoke(this, e);
        }

        private void DoneButton_Clicked(object sender, EventArgs e)
        {
            //TODO: MATEO go to the calendar and add the peiod selected to the database.
        }
    }
}