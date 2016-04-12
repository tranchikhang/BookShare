using System;
using Windows.UI;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace BookShare.Helper.Converter
{
	class NewMessageColorConverter : IValueConverter
	{
		object IValueConverter.Convert ( object value , Type targetType , object parameter , string language )
		{
			return ( ( bool ) value ) ? new SolidColorBrush ( Colors.Blue ) : new SolidColorBrush ( Colors.Black );
		}

		object IValueConverter.ConvertBack ( object value , Type targetType , object parameter , string language )
		{
			return ( ( bool ) value ) ? new SolidColorBrush ( Colors.Black ) : new SolidColorBrush ( Colors.Blue );
		}
	}
}
