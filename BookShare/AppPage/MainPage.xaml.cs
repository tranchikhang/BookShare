using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
	public sealed partial class MainPage : Page
	{
		public MainPage ()
		{
			this.InitializeComponent ();
			( ( App ) Application.Current ).MainFrame = mainFrame;
		}

		protected override void OnNavigatedTo ( NavigationEventArgs e )
		{
			base.OnNavigatedTo ( e );
			MainSplitView.IsPaneOpen = false;
			if ( MainSplitView.Content != null )
				mainFrame.Navigate ( typeof ( GreetingPage ) );
		}

		private void HamburgerClick ( object sender , RoutedEventArgs e )
		{
			MainSplitView.IsPaneOpen = !MainSplitView.IsPaneOpen;
		}

		private void HomeClick ( object sender , RoutedEventArgs e )
		{
			MainSplitView.IsPaneOpen = false;
			if ( MainSplitView.Content != null )
				mainFrame.Navigate ( typeof ( GreetingPage ) );
		}

		private void SettingClick ( object sender , RoutedEventArgs e )
		{
			MainSplitView.IsPaneOpen = false;
			if ( MainSplitView.Content != null )
				mainFrame.Navigate ( typeof ( SettingPage ) );
		}	

		private void RequestListClick ( object sender , RoutedEventArgs e )
		{
			MainSplitView.IsPaneOpen = false;
			if ( MainSplitView.Content != null )
				mainFrame.Navigate ( typeof ( RequestList ) );
		}

		private void BookShelfClick ( object sender , RoutedEventArgs e )
		{
			MainSplitView.IsPaneOpen = false;
			if ( MainSplitView.Content != null )
				mainFrame.Navigate ( typeof ( BookShelf ) );
		}

		private void MessageListClick ( object sender , RoutedEventArgs e )
		{
			MainSplitView.IsPaneOpen = false;
			if ( MainSplitView.Content != null )
				mainFrame.Navigate ( typeof ( MessagePage ) );
		}
	}
}
