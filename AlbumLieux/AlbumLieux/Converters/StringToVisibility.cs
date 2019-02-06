using System;
using System.Globalization;
using Xamarin.Forms;
namespace AlbumLieux.Converters
{
	/// <summary>
	/// Si la chaine est vide on souhaite cacher l'élément
	/// </summary>
	public class StringToVisibility : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value == null)
			{
				return false;
			}
			else if (value is string s)
			{
				return !string.IsNullOrEmpty(s);
			}
			else
			{
				throw new NotSupportedException();
			}
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
