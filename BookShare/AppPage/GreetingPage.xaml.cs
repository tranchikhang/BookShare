using BookShare.Helper;
using BookShare.Model;
using Newtonsoft.Json.Linq;
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
	public sealed partial class GreetingPage : Page
	{
		public GreetingPage ()
		{
			this.InitializeComponent ();
		}

		private int numberOfRandomBooks = 4;
		private int numberOfNewestBooks = 4;

		protected override void OnNavigatedTo ( NavigationEventArgs e )
		{
			//get random books
			GetRandomBooks ( numberOfRandomBooks );
			//get newest books
		}

		List<object> randomBook;

		private async void GetRandomBooks ( int numberOfBooks )
		{
			string result = await RestAPI.SendJson ( numberOfBooks , RestAPI.phpAddress , "GetRandomBooks" );
			dynamic json = JObject.Parse ( result );
			string status = json.status;
			if ( status == "200" )
			{
				randomBook = new List<object> ();
				for ( int i = 0 ; i < json.randomBooks.Count ; i++ )
				{
					randomBook.Add ( new
					{
						bookId = json.randomBooks[i].bookId ,
						title = json.randomBooks[i].title ,
						author = json.randomBooks[i].author ,
						image = RestAPI.serverAddress + "cover/" + json.randomBooks[i].bookId + ".jpg" ,
						numberShared = json.randomBooks[i].numberShared
					} );
				}
				listViewBooks.ItemsSource = randomBook;
			}
		}

		private void TitleTapped ( object sender , TappedRoutedEventArgs e )
		{
			string value = ( ( TextBlock ) sender ).Tag.ToString ();
			Frame.Navigate ( typeof ( BookInfo ) , value );
		}

		private void SearchBox_GotFocus ( object sender , RoutedEventArgs e )
		{
			if ( SearchBox.Text == "Nhập nội dung tìm kiếm" )
				SearchBox.Text = "";
		}

		private void SearchClick ( object sender , RoutedEventArgs e )
		{
			string query = SearchBox.Text;
			Frame.Navigate ( typeof ( SearchPage ) , query );
		}
	}
}
