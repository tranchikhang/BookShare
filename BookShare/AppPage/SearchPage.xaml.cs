using BookShare.Helper;
using BookShare.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
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
	public sealed partial class SearchPage : Page
	{
		public SearchPage ()
		{
			this.InitializeComponent ();
		}

		private void SearchClick ( object sender , RoutedEventArgs e )
		{
			string query = SearchBox.Text;
			SendSearchQuery ( query );
		}

		private async void SendSearchQuery ( string query )
		{
			string result = await RestAPI.SendJson ( query , RestAPI.phpAdress + "client/book/getbook.php" , "GetSearchResult" );
			if ( result == "empty" )
			{
				//no book
				MessageDialog dialog = new MessageDialog ( result );
				await dialog.ShowAsync ();
			}
			else
			{
				List<BookView> listBook = new List<BookView> ();
				listBook = JsonConvert.DeserializeObject<List<BookView>> ( result );
				//set image link
				foreach (BookView bv in listBook)
				{
					bv.SetImageLink ();
				}
				listBoxResults.ItemsSource = listBook;
				//MessageDialog dialog = new MessageDialog ( result );
				//await dialog.ShowAsync ();
			}
		}

		private void TitleTapped ( object sender , TappedRoutedEventArgs e )
		{
			string value = ( ( TextBlock ) sender ).Tag.ToString ();
			Frame.Navigate ( typeof ( BookInfo ) , value );
		}

		private void SearchBox_GotFocus ( object sender , RoutedEventArgs e )
		{
			SearchBox.Text = "";
		}
	}
}
