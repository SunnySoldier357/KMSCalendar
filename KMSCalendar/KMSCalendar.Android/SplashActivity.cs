using System.Threading.Tasks;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Util;

namespace KMSCalendar.Droid
{
	[Activity(Theme = "@style/MyTheme.Splash", 
		MainLauncher = true, 
		NoHistory = true)]
	public class SplashActivity : Activity
	{
		//* Static Properties
		private static string TAG => "X:" + nameof(SplashActivity);

		//* Overridden Methods

		// Prevent the back button from canceling the startup process
		public override void OnBackPressed() { }

		public override void OnCreate(Bundle savedInstanceState, PersistableBundle persistentState)
		{
			base.OnCreate(savedInstanceState, persistentState);
			Log.Debug(TAG, "SplashActivity.OnCreate");
		}

		// Launches the startup task
		protected override void OnResume()
		{
			base.OnResume();
			Task startupWork = new Task(() => { SimulateStartup(); });
			startupWork.Start();
		}

		//* Private Methods

		// Simulates background work that happens behind the splash screen
		private async void SimulateStartup()
		{
			Log.Debug(TAG, "Performing some startup work that takes a bit of time.");
			await Task.Delay(8000); // Simulate a bit of startup work.
			Log.Debug(TAG, "Startup work is finished - starting MainActivity.");
			StartActivity(new Intent(Application.Context, typeof(MainActivity)));
		}
	}
}