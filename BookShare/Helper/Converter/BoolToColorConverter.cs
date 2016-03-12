using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace BookShare.Helper.Converter
{
	class BoolToColorConverter : IValueConverter
	{
		object IValueConverter.Convert ( object value , Type targetType , object parameter , string language )
		{
			return ( ( bool ) value ) ? new SolidColorBrush ( Colors.LightBlue ) : new SolidColorBrush ( Colors.LightGreen );
		}

		object IValueConverter.ConvertBack ( object value , Type targetType , object parameter , string language )
		{
			return ( ( bool ) value ) ? new SolidColorBrush ( Colors.LightGreen ) : new SolidColorBrush ( Colors.LightBlue );
		}
	}
}
