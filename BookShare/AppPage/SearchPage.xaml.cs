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
		}

		protected override void OnNavigatedTo ( NavigationEventArgs e )
		{
			string query = e.Parameter as String;
			SendSearchQuery ( query );
		}

		private async void SendSearchQuery ( string query )
		{
			string result = await RestAPI.SendJson ( query , RestAPI.phpAddress , "GetSearchResult" );
			if ( result == "empty" )
			{
				//no book to display
				//option to add new book
				stackPanelAddNew.Visibility = Windows.UI.Xaml.Visibility.Visible;
			}
			else
			{
				dynamic json = JArray.Parse ( result );
				var l = new List<object> ();
				for ( int i = 0 ; i < json.Count ; i++ )
				{
					l.Add ( new
					{
						bookid = json[i].bookid ,
						book = json[i].book ,
						authorid = json[i].authorid ,
						author = json[i].author ,
						image = RestAPI.serverAddress + "cover/" + json[i].bookid + ".jpg"
					} );
				}
				listBoxResults.ItemsSource = l;
			}
		}

		private void TitleTapped ( object sender , TappedRoutedEventArgs e )
		{
			string value = ( ( TextBlock ) sender ).Tag.ToString ();
			Frame.Navigate ( typeof ( BookInfo ) , value );
		}

		private void AddNewBook ( object sender , Windows.UI.Xaml.RoutedEventArgs e )
		{
			Frame.Navigate ( typeof ( AddNewBook ) );
		}

		private void SearchBox_GotFocus ( object sender , Windows.UI.Xaml.RoutedEventArgs e )
		{
			if ( SearchBox.Text == "Nhập nội dung tìm kiếm" )
				SearchBox.Text = "";
		}

		private void SearchClick ( object sender , Windows.UI.Xaml.RoutedEventArgs e )
		{
			SendSearchQuery ( SearchBox.Text );
		}
	}
}
