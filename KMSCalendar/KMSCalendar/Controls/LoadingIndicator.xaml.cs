using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KMSCalendar.Controls
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoadingIndicator : ContentView
	{
		public static readonly BindableProperty IsLoadingProperty = BindableProperty.Create(nameof(IsLoading), typeof(bool), typeof(LoadingIndicator), default(bool), propertyChanged: HandleIsRunningPropertyChanged);
		public static readonly BindableProperty LoadingIndicatorColorProperty = BindableProperty.Create(nameof(LoadingIndicatorColor), typeof(Color), typeof(LoadingIndicator), default(Color), propertyChanged: HandleSpinnerColorPropertyChanged);
		private const double FullyOpaque = 1;
		private const double FullyTransparent = 0;
		private const uint TogglingVisibilityAnimationDuration = 400;

		private static readonly SemaphoreSlim ToggleVisibilityAnimationSemaphoreSlim = new SemaphoreSlim(1);

		public LoadingIndicator() => InitializeComponent();

		public bool IsLoading
		{
			get => (bool) GetValue(IsLoadingProperty);
			set => SetValue(IsLoadingProperty, value);
		}

		public Color LoadingIndicatorColor
		{
			get => (Color) GetValue(LoadingIndicatorColorProperty);
			set => SetValue(LoadingIndicatorColorProperty, value);
		}

		private static async void HandleIsRunningPropertyChanged(BindableObject bindable, object oldValue, object newValue)
		{
			if (!(bindable is LoadingIndicator customActivityIndicator) || !(newValue is bool isRunning))
			{
				return;
			}

			await ToggleVisibility(customActivityIndicator);
		}

		private static void HandleSpinnerColorPropertyChanged(BindableObject bindable, object oldValue, object newValue)
		{
			if (!(bindable is LoadingIndicator customActivityIndicator) || !(newValue is Color spinnerColor))
			{
				return;
			}

			customActivityIndicator.LoadingIndicatorSpinner.Color = spinnerColor;
		}

		private static async Task ToggleVisibility(LoadingIndicator customActivityIndicator)
		{
			try
			{
				ViewExtensions.CancelAnimations(customActivityIndicator);

				await ToggleVisibilityAnimationSemaphoreSlim.WaitAsync();
				if (customActivityIndicator.IsLoading)
				{
					customActivityIndicator.LoadingIndicatorSpinner.IsRunning = true;
					//customActivityIndicator.IsVisible = true;
					await customActivityIndicator.FadeTo(FullyOpaque, TogglingVisibilityAnimationDuration);
				}
				else
				{
					await customActivityIndicator.FadeTo(FullyTransparent, TogglingVisibilityAnimationDuration);
					customActivityIndicator.LoadingIndicatorSpinner.IsRunning = false;
					//customActivityIndicator.IsVisible = false;
				}
			}
			finally
			{
				ToggleVisibilityAnimationSemaphoreSlim.Release();
			}
		}
	}
}