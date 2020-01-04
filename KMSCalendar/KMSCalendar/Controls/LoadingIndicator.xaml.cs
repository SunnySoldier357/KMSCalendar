using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KMSCalendar.Controls
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoadingIndicator : ContentView
	{
		//* Static Properties
		private static readonly SemaphoreSlim ToggleVisibilityAnimationSemaphoreSlim =
			new SemaphoreSlim(1);

		public static readonly BindableProperty IsLoadingProperty = BindableProperty.Create(
			propertyName: nameof(IsLoading),
			returnType: typeof(bool),
			declaringType: typeof(LoadingIndicator),
			defaultValue: default(bool),
			propertyChanged: HandleIsRunningPropertyChanged);

		public static readonly BindableProperty LoadingIndicatorColorProperty = BindableProperty.Create(
			propertyName: nameof(LoadingIndicatorColor),
			returnType: typeof(Color),
			declaringType: typeof(LoadingIndicator),
			defaultValue: default(Color),
			propertyChanged: HandleSpinnerColorPropertyChanged);

		//* Constants
		private const double FULLY_OPAQUE = 1;
		private const double FULLY_TRANSPARENT = 0;

		private const uint TOGGLE_ANIMATION_DURATION = 400;

		//* Constructors
		public LoadingIndicator() => InitializeComponent();

		//* Public Properties
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

		//* Event Handlers
		private static async void HandleIsRunningPropertyChanged(BindableObject bindable,
			object oldValue, object newValue)
		{
			if (bindable is LoadingIndicator customActivityIndicator && newValue is bool isRunning)
				await ToggleVisibilityAsync(customActivityIndicator);
		}

		private static void HandleSpinnerColorPropertyChanged(BindableObject bindable, object oldValue, object newValue)
		{
			if (bindable is LoadingIndicator customActivityIndicator && newValue is Color spinnerColor)
				customActivityIndicator.LoadingIndicatorSpinner.Color = spinnerColor;
		}

		//* Static Methods
		private static async Task ToggleVisibilityAsync(LoadingIndicator customActivityIndicator)
		{
			try
			{
				ViewExtensions.CancelAnimations(customActivityIndicator);

				await ToggleVisibilityAnimationSemaphoreSlim.WaitAsync();

				customActivityIndicator.LoadingIndicatorSpinner.IsRunning =
					customActivityIndicator.IsLoading;

				if (customActivityIndicator.IsLoading)
					await customActivityIndicator.FadeTo(FULLY_OPAQUE, TOGGLE_ANIMATION_DURATION);
				else
					await customActivityIndicator.FadeTo(FULLY_TRANSPARENT, TOGGLE_ANIMATION_DURATION);
			}
			finally
			{
				ToggleVisibilityAnimationSemaphoreSlim.Release();
			}
		}
	}
}