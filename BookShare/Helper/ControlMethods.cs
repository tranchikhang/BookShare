using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace BookShare.Helper
{
	class ControlMethods
	{
		public static void SwitchVisibility ( bool v , Control control )
		{
			if ( v )
				control.Visibility = Windows.UI.Xaml.Visibility.Visible;
			else
				control.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
		}
	}
}
