using BookShare.Helper;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Xml;
using Windows.UI.Notifications;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml;
using System.Collections.ObjectModel;
using BookShare.Model;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace BookShare.AppPage
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class SearchPage : Page
	{
		public SearchPage ()
		{
			this.InitializeComponent ();
			ControlMethods.SwitchVisibility ( false , listViewResults );
			stackPanelAddNew.Visibility = Visibility.Collapsed;

			timer = new DispatcherTimer ();
			timer.Interval = TimeSpan.FromSeconds ( 1 );
			timer.Tick += new EventHandler<object> ( TimerTick );
		}

		private void TimerTick ( object sender , object e )
		{
			timer.Stop ();
			//hide progress bar
			ControlMethods.SwitchVisibility ( false , progressBar );
		}

		protected override void OnNavigatedTo ( NavigationEventArgs e )
		{
			query = ( string ) e.Parameter;
			SendSearchQuery ( query );
		}

		public string query;
		private ObservableCollection<Book> books;
		DispatcherTimer timer;

		private async void SendSearchQuery ( string query )
		{
			ControlMethods.SwitchVisibility ( true , progressBar );
			timer.Start ();
			string result = await RestAPI.SendJson ( query , RestAPI.phpAddress , "GetSearchResult" );
			if ( JsonHelper.IsRequestSucceed ( result ) == RestAPI.ResponseStatus.Empty )
			{
				//no book to display
				//show option to add new book
				ControlMethods.SwitchVisibility ( false , progressBar );
				ControlMethods.SwitchVisibility ( false , listViewResults );
				stackPanelAddNew.Visibility = Visibility.Visible;
			}
			else if ( JsonHelper.IsRequestSucceed ( result ) == RestAPI.ResponseStatus.OK )
			{
				//result is not empty
				string data = JsonHelper.DecodeJson ( result );
				books = JsonHelper.ConvertToBooks ( data );
				foreach ( Book b in books )
				{
					b.SetImageLink ();
				}
				listViewResults.ItemsSource = books;
				//if timer is still running, then do nothing
				if ( !timer.IsEnabled )
					ControlMethods.SwitchVisibility ( false , progressBar );
				stackPanelAddNew.Visibility = Visibility.Collapsed;
				ControlMethods.SwitchVisibility ( true , listViewResults );
			}
		}

		private void TitleTapped ( object sender , TappedRoutedEventArgs e )
		{
			string value = ( ( TextBlock ) sender ).Tag.ToString ();
			Frame.Navigate ( typeof ( BookInfo ) , value );
		}

		private void AddNewBook ( object sender , RoutedEventArgs e )
		{
			Frame.Navigate ( typeof ( AddNewBook ) );
		}

		private void SearchClick ( object sender , RoutedEventArgs e )
		{
			SendSearchQuery ( SearchBox.Text );
		}
	}
}
