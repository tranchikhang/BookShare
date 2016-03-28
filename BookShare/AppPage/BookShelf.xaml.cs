using BookShare.Helper;
using BookShare.Model;
using System.Collections.ObjectModel;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
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
			//string result = await RestAPI.SendJson ( UserData.id , RestAPI.phpAddress , "GetPostedBooks" );
			string result =
				await RestAPI.SendPostRequest ( UserData.id , RestAPI.publicApiAddress + "booklist/" );
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
				gridNotification.Show ( false , "Không có sách, hãy thêm sách bằng cách tìm kiếm" );
			}
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
			//string result = await RestAPI.SendJson ( postId , RestAPI.phpAddress , "SwitchAvailability" );
			string result =
				await RestAPI.SendPostRequest ( postId , RestAPI.publicApiAddress + "booklist/available/" );
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

		private async void RemoveBook ( object sender , RoutedEventArgs e )
		{
			ControlMethods.SwitchVisibility ( true , progressBar );
			string bookId = ( ( Button ) sender ).Tag.ToString ();
			dynamic book = new
			{
				bookId = bookId ,
				userId = UserData.id
			};
			//string addResult = await RestAPI.SendJson ( book , RestAPI.phpAddress , "RemoveFromYourBook" );
			string addResult =
					await RestAPI.SendPostRequest ( book , RestAPI.publicApiAddress + "booklist/remove/" );
			if ( JsonHelper.IsRequestSucceed ( addResult ) == RestAPI.ResponseStatus.OK )
			{
				postedBooks.Remove ( postedBooks.First ( p => p.book.id == bookId ) );
			}
			else
			{
				gridNotification.Show ( true );
			}
			ControlMethods.SwitchVisibility ( false , progressBar );
		}
	}
}
