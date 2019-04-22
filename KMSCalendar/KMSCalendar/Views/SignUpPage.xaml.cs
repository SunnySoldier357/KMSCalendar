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
	public partial class SignUpPage : ContentPage
	{
		public SignUpPage ()
		{
			InitializeComponent ();
		}

        private void AlreadyUserButton_Clicked(object sender, EventArgs e)
        {
            //do something with the sign up info.
        }

        private void AuthenticateSignUpButton_Clicked(object sender, EventArgs e)
        {
            //redirect to log in page.
        }
    }
}