using System;
using Windows.UI.Xaml.Data;

namespace BookShare.Helper.Converter
{
	class IntToButtonContentConverter : IValueConverter
	{
		object IValueConverter.Convert ( object value , Type targetType , object parameter , string language )
		{
			if ( int.Parse ( value.ToString () ) == -1 )
			{
				return "Đã được mượn";
			}
			else if ( int.Parse ( value.ToString () ) == 0 )
			{
				return "Mượn";
			}
			else return "Chờ chấp nhận";
		}

		object IValueConverter.ConvertBack ( object value , Type targetType , object parameter , string language )
		{
			if ( int.Parse ( value.ToString () ) == -1 )
			{
				return "Đã được mượn";
			}
			else if ( int.Parse ( value.ToString () ) == 0 )
			{
				return "Mượn";
			}
			else return "Chờ chấp nhận";
		}
	}
}
