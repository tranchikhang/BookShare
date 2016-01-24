using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace BookShare.AppPage
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class StartPage : Page
	{
		public StartPage ()
		{
			this.InitializeComponent ();
			//default size
			ApplicationView.PreferredLaunchViewSize = new Size ( 480 , 640 );
			ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;
			//minimum resize
			ApplicationView.GetForCurrentView ().SetPreferredMinSize ( new Size ( 480 , 500 ) );
		}

		private void RegisterClick ( object sender , RoutedEventArgs e )
		{
			this.Frame.Navigate ( typeof ( AppPage.Register ) );
		}

		private void LoginClick ( object sender , RoutedEventArgs e )
		{
			this.Frame.Navigate ( typeof ( AppPage.Login ) );
		}
	}
}
