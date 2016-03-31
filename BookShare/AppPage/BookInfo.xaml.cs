using BookShare.Helper;
using BookShare.Model;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace BookShare.AppPage
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class BookInfo : Page
	{
		public BookInfo ()
		{
			this.InitializeComponent ();

			NavigationMethod.SetBackButtonVisibility ( true );
			SystemNavigationManager.GetForCurrentView ().BackRequested += BackButtonClick;

		}

		private string bookId;
		private Book selectedBook;
		private ObservableCollection<Post> lenders;

		protected override void OnNavigatedTo ( NavigationEventArgs e )
		{
			bookId = e.Parameter as String;
			LoadBookInfo ();
		}

		protected override void OnNavigatedFrom ( NavigationEventArgs e )
		{
			SystemNavigationManager.GetForCurrentView ().BackRequested -= BackButtonClick;
		}

		private async void LoadBookInfo ()
		{
			string bookInfo = await RestAPI.SendPostRequest ( UserData.id , RestAPI.publicApiAddress + "book/" + bookId );
			string bookLenders =
				await RestAPI.SendPostRequest ( UserData.id , RestAPI.publicApiAddress + "book/" + bookId + "/lenders/" );

			//deserialize json into book
			if ( JsonHelper.IsRequestSucceed ( bookInfo ) == RestAPI.ResponseStatus.OK )
			{
				string data = JsonHelper.DecodeJson ( bookInfo );
				selectedBook = JsonHelper.ConvertToBook ( data );
			}
			selectedBook.SetImageLink ();
			listViewRelatedBooks.ItemsSource = selectedBook.relatedBooks;
			//if book exist in user book list, change button content
			if ( selectedBook.isBookAdded )
			{
				buttonAddBook.Content = "Xóa";
				buttonAddBook.Tag = 1;
				//#F44336
				buttonAddBook.Background = new SolidColorBrush ( Color.FromArgb ( 255 , 244 , 67 , 54 ) );
			}
			else
			{
				buttonAddBook.Content = "Thêm";
				buttonAddBook.Tag = 0;
				//#4CAF50
				buttonAddBook.Background = new SolidColorBrush ( Color.FromArgb ( 255 , 76 , 175 , 80 ) );
			}
			//deserialize json into lenders
			if ( JsonHelper.IsRequestSucceed ( bookLenders ) == RestAPI.ResponseStatus.OK )
			{
				string data = JsonHelper.DecodeJson ( bookLenders );
				lenders = JsonHelper.ConvertToPosts ( data );
			}
			if ( lenders != null && lenders.Count > 0 )
			{
				foreach ( Post p in lenders )
				{
					p.user.SetAddress ();
					p.user.SetAva ();
				}
			}

			//set binding source
			listViewLenders.ItemsSource = lenders;
			relativePanelInfo.DataContext = selectedBook;

			ControlMethods.SwitchVisibility ( false , progressBar );
			mainScrollViewer.Visibility = Visibility.Visible;
			ControlMethods.SwitchVisibility ( true , listViewLenders );
		}

		private async void SendRequest ( object sender , RoutedEventArgs e )
		{
			string postId = ( ( Button ) sender ).Tag.ToString ();
			ControlMethods.SwitchVisibility ( true , progressBar );
			await SendRequestToPost ( postId , sender );
			ControlMethods.SwitchVisibility ( false , progressBar );
		}

		private async Task SendRequestToPost ( string postId , object sender )
		{
			dynamic request = new
			{
				postId = postId ,
				userId = UserData.id
			};
			string result =
				await RestAPI.SendPostRequest ( request , RestAPI.publicApiAddress + "request/new/" );
			if ( JsonHelper.IsRequestSucceed ( result ) == RestAPI.ResponseStatus.OK )
			{
				gridNotification.Show ( false , "Đã gửi yêu cầu" );
				( ( Button ) sender ).IsEnabled = false;
			}
			else
			{
				gridNotification.Show ( true );
			}
		}

		private async void AddToYourBook ( object sender , RoutedEventArgs e )
		{
			ControlMethods.SwitchVisibility ( true , progressBar );
			if ( ( ( Button ) sender ).Tag.ToString () == "0" )
			{
				//add book
				dynamic book = new
				{
					bookId = bookId ,
					userId = UserData.id
				};
				string result =
					await RestAPI.SendPutRequest ( book , RestAPI.publicApiAddress + "booklist/add/" );
				if ( JsonHelper.IsRequestSucceed ( result ) == RestAPI.ResponseStatus.OK )
				{
					( ( Button ) sender ).Content = "Xóa";
					( ( Button ) sender ).Tag = 1;
					//#F44336
					( ( Button ) sender ).Background = new SolidColorBrush ( Color.FromArgb ( 255 , 244 , 67 , 54 ) );
				}
				else
				{
					gridNotification.Show ( true );
				}
			}
			else
			{
				//remove book
				dynamic book = new
				{
					bookId = bookId ,
					userId = UserData.id
				};
				string result =
					await RestAPI.SendPostRequest ( book , RestAPI.publicApiAddress + "booklist/remove/" );
				if ( JsonHelper.IsRequestSucceed ( result ) == RestAPI.ResponseStatus.OK )
				{
					( ( Button ) sender ).Content = "Thêm";
					( ( Button ) sender ).Tag = 0;
					//#4CAF50
					( ( Button ) sender ).Background = new SolidColorBrush ( Color.FromArgb ( 255 , 76 , 175 , 80 ) );
				}
				else
				{
					gridNotification.Show ( true );
				}
			}
			ControlMethods.SwitchVisibility ( false , progressBar );
		}

		private void UserAccountTapped ( object sender , TappedRoutedEventArgs e )
		{
			string userId = ( ( TextBlock ) sender ).Tag.ToString ();
			Frame.Navigate ( typeof ( UserInfo ) , userId );
		}

		private void BackButtonClick ( object sender , BackRequestedEventArgs e )
		{
			Frame rootFrame = NavigationMethod.GetMainFrame ();

			// Navigate back if possible, and if the event has not 
			// already been handled .
			if ( rootFrame.CanGoBack )
			{
				rootFrame.GoBack ();
			}
		}
	}
}
