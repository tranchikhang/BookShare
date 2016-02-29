using BookShare.Helper;
using BookShare.Model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
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
	public sealed partial class BookShelf : Page
	{
		public BookShelf ()
		{
			this.InitializeComponent ();
			progressBar.Visibility = Visibility.Visible;
			listBoxPostedBooks.Visibility = Visibility.Collapsed;
		}

		protected override void OnNavigatedTo ( NavigationEventArgs e )
		{
			GetPostedBooks ();
		}

		private List<object> postedBooks;

		private async void GetPostedBooks ()
		{
			string result = await RestAPI.SendJson ( UserData.id , RestAPI.phpAddress , "GetPostedBooks" );
			dynamic json = JObject.Parse ( result );
			string status = json.status;
			if ( status == "200" )
			{
				postedBooks = new List<object> ();
				for ( int i = 0 ; i < json.postedBooks.Count ; i++ )
				{
					postedBooks.Add ( new
					{
						postId = json.postedBooks[i].postId ,
						isAvailable = ( json.postedBooks[i].available == 1 ) ? true : false ,
						title = json.postedBooks[i].title ,
						image = RestAPI.serverAddress + "cover/" + json.postedBooks[i].bookId + ".jpg" ,
						author = json.postedBooks[i].author
					} );
				}
				listBoxPostedBooks.ItemsSource = postedBooks;
			}
			else
			{
				//no book in bookshelf
				listBoxPostedBooks.Visibility = Visibility.Collapsed;
				stackPanelAddNew.Visibility = Visibility.Visible;
			}
			progressBar.Visibility = Visibility.Collapsed;
			listBoxPostedBooks.Visibility = Visibility.Visible;
		}

		private void ToggleLoaded ( object sender , RoutedEventArgs e )
		{
			ToggleSwitch toggle = ( ToggleSwitch ) sender;
			toggle.Toggled += ToggleSwitch_Toggled;
		}

		private async void ToggleSwitch_Toggled ( object sender , RoutedEventArgs e )
		{
			string postId = ( ( ToggleSwitch ) sender ).Tag.ToString ();
			( ( ToggleSwitch ) sender ).IsEnabled = false;
			dynamic json = await SwitchAvailability ( postId );
			string status = json.status;
			string message = json.message;
			if ( status != "200" )
			{
				CustomNotification.ShowDialogMessage ( status , message );
			}
			( ( ToggleSwitch ) sender ).IsEnabled = true;
		}

		private async Task<dynamic> SwitchAvailability ( string postId )
		{
			string result = await RestAPI.SendJson ( postId , RestAPI.phpAddress , "SwitchAvailability" );
			dynamic json = JObject.Parse ( result );
			return json;
		}
	}
}
