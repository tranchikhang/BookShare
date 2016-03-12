using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace BookShare.Helper.Converter
{
	class BoolToPositionConverter : IValueConverter
	{
		object IValueConverter.Convert ( object value , Type targetType , object parameter , string language )
		{
			return ( ( bool ) value ) ? TextAlignment.Left : TextAlignment.Right;
		}

		object IValueConverter.ConvertBack ( object value , Type targetType , object parameter , string language )
		{
			return ( ( bool ) value ) ? TextAlignment.Right : TextAlignment.Left;
		}
	}
}
