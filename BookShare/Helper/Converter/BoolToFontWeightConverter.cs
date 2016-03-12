using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace BookShare.Helper.Converter
{
	public class BoolToFontWeightConverter : IValueConverter
	{

		object IValueConverter.Convert ( object value , Type targetType , object parameter , string language )
		{
			return ( ( bool ) value ) ? FontWeights.Bold : FontWeights.Normal;
		}

		object IValueConverter.ConvertBack ( object value , Type targetType , object parameter , string language )
		{
			return ( ( bool ) value ) ? FontWeights.Bold : FontWeights.Normal;
		}
	}
}
