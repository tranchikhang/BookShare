using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace BookShare.Helper.Converter
{
	class BoolToHorizontalAlignmentConverter : IValueConverter
	{
		object IValueConverter.Convert ( object value , Type targetType , object parameter , string language )
		{
			return ( ( bool ) value ) ? HorizontalAlignment.Left : HorizontalAlignment.Right;
		}

		object IValueConverter.ConvertBack ( object value , Type targetType , object parameter , string language )
		{
			return ( ( bool ) value ) ? HorizontalAlignment.Right : HorizontalAlignment.Left;
		}
	}
}
