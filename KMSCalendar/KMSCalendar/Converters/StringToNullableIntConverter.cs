using System;
using System.Globalization;

using Xamarin.Forms;

namespace KMSCalendar.Converters
{
	public class StringToNullableIntConverter : IValueConverter
	{
		//* Interface Implementations
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			bool isStringConvertable = int.TryParse(value as string, out int result);

			return isStringConvertable ? (int?) result : null;
		}
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
			value == null ? null : $"{(int) value}";
	}
}