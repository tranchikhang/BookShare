using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls;

namespace BookShare.Helper
{
	class NavigationMethod
	{
		public static Frame GetMainFrame ()
		{
			return ( App.Current as App ).MainFrame;
		}

		public static Frame GetTopFrame ()
		{
			return ( App.Current as App ).TopFrame;
		}

		public static void SetMainFrame ( Frame f )
		{
			( App.Current as App ).MainFrame = f;
		}

		public static void SetTopFrame ( Frame f )
		{
			( App.Current as App ).TopFrame = f;
		}

		public static void SetBackButtonVisibility ( bool isShow )
		{
			if ( isShow )
				//show back button
				SystemNavigationManager.GetForCurrentView ().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
			else
				//show back button
				SystemNavigationManager.GetForCurrentView ().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Collapsed;
		}
	}
}
