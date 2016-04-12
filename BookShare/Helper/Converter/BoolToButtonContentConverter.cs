using System;
using Windows.UI.Xaml.Data;

namespace BookShare.Helper.Converter
{
	class BoolToButtonContentConverter : IValueConverter
	{
		object IValueConverter.Convert ( object value , Type targetType , object parameter , string language )
		{
			return ( ( bool ) value ) ? "Mượn" : "Đã được mượn";
		}

		object IValueConverter.ConvertBack ( object value , Type targetType , object parameter , string language )
		{
			return ( ( bool ) value ) ? "Đã được mượn" : "Mượn";
		}
	}
}
