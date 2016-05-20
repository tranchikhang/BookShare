using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace BookShare.Helper.Converter
{
	class IntToBoolConverter : IValueConverter
	{
		object IValueConverter.Convert ( object value , Type targetType , object parameter , string language )
		{
			if ( int.Parse ( value.ToString () ) == 0 )
			{
				return true;
			}
			return false;
		}

		object IValueConverter.ConvertBack ( object value , Type targetType , object parameter , string language )
		{
			if ( int.Parse ( value.ToString () ) == 0 )
			{
				return true;
			}
			return false;
		}
	}
}
