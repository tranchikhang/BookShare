using BookShare.Helper;
using BookShare.Model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
			GetPostedBooks ();
		}

		protected override void OnNavigatedTo ( NavigationEventArgs e )
		{

		}

		private ObservableCollection<Post> postedBooks;

		private async void GetPostedBooks ()
		{
			string result = await RestAPI.SendJson ( UserData.id , RestAPI.phpAddress , "GetPostedBooks" );
			if ( JsonHelper.IsRequestSucceed ( result ) == RestAPI.ResponseStatus.OK )
			{
				string data = JsonHelper.DecodeJson ( result );
				postedBooks = JsonHelper.ConvertToPosts ( data );
				if ( postedBooks != null && postedBooks.Count > 0 )
					foreach ( Post p in postedBooks )
					{
						p.book.SetImageLink ();
					}
				listBoxPostedBooks.ItemsSource = postedBooks;
				listBoxPostedBooks.Visibility = Visibility.Visible;
			}
			else
			{
				//no book in bookshelf
				ShowNotification ( "Không có sách, hãy thêm sách bằng cách tìm kiếm" );
			}
			if ( result != null )
				progressBar.Visibility = Visibility.Collapsed;
		}

		private void ToggleLoaded ( object sender , RoutedEventArgs e )
		{
			ToggleSwitch toggle = ( ToggleSwitch ) sender;
			toggle.Toggled += ToggleSwitch_Toggled;
		}

		private async void ToggleSwitch_Toggled ( object sender , RoutedEventArgs e )
		{
			ControlMethods.SwitchVisibility ( true , progressBar );
			string postId = ( ( ToggleSwitch ) sender ).Tag.ToString ();
			( ( ToggleSwitch ) sender ).IsEnabled = false;
			string result = await RestAPI.SendJson ( postId , RestAPI.phpAddress , "SwitchAvailability" );
			if ( JsonHelper.IsRequestSucceed ( result ) == RestAPI.ResponseStatus.OK )
			{
				//
			}
			if ( result != null )
			{
				ControlMethods.SwitchVisibility ( false , progressBar );
				( ( ToggleSwitch ) sender ).IsEnabled = true;
			}
		}

		private void ShowNotification ( string content )
		{
			//notify user
			textBlockContent.Text = content;
			gridNotification.Visibility = Visibility.Visible;
		}

		private void NotificationDismiss ( object sender , RoutedEventArgs e )
		{
			textBlockContent.Text = "";
			gridNotification.Visibility = Visibility.Collapsed;
		}
	}
}
