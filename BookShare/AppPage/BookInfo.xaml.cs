using BookShare.Helper;
using BookShare.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
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
			ControlMethods.SwitchVisibility ( true , progressBar );
			relativePanelInfo.Visibility = Visibility.Collapsed;
			ControlMethods.SwitchVisibility ( false , listViewLenders );
		}

		private string bookId;
		private Book selectedBook;
		private ObservableCollection<Post> lenders;

		protected override void OnNavigatedTo ( NavigationEventArgs e )
		{
			bookId = e.Parameter as String;
			LoadBookInfo ();
			DisplayBackButton ();
		}

		private void DisplayBackButton ()
		{
			Frame rootFrame = ( ( App ) Application.Current ).MainFrame;
			if ( rootFrame.CanGoBack )
			{
				SystemNavigationManager.GetForCurrentView ().AppViewBackButtonVisibility =
					AppViewBackButtonVisibility.Visible;
			}
			else
			{
				SystemNavigationManager.GetForCurrentView ().AppViewBackButtonVisibility =
					AppViewBackButtonVisibility.Collapsed;
			}
		}

		private async void LoadBookInfo ()
		{
			//send request with book id and user id
			dynamic bookToSend = new
			{
				bookId = bookId ,
				userId = UserData.id
			};
			string bookInfo = await RestAPI.SendJson ( bookToSend , RestAPI.phpAddress , "GetBookById" );

			dynamic getLenders = new
			{
				bookId = bookId ,
				userId = UserData.id
			};
			string bookLenders = await RestAPI.SendJson ( getLenders , RestAPI.phpAddress , "GetLenderForBook" );

			//deserialize json into book
			if ( JsonHelper.IsRequestSucceed ( bookInfo ) == RestAPI.ResponseStatus.OK )
			{
				string data = JsonHelper.DecodeJson ( bookInfo );
				selectedBook = JsonHelper.ConvertToBook ( data );
			}
			selectedBook.SetImageLink ();
			BookCover.Source = new BitmapImage ( new Uri ( @selectedBook.image ) );
			//if book exist in user book list, change button content
			if ( selectedBook.isBookAdded )
			{
				buttonAddBook.Content = "Xóa";
				buttonAddBook.Tag = 0;
			}
			else
			{
				buttonAddBook.Content = "Thêm";
				buttonAddBook.Tag = 1;
			}
			//deserialize json into lenders
			if ( JsonHelper.IsRequestSucceed ( bookLenders ) == RestAPI.ResponseStatus.OK )
			{
				string data = JsonHelper.DecodeJson ( bookLenders );
				lenders = JsonHelper.ConvertToPosts ( data );
			}
			foreach ( Post p in lenders )
			{
				p.user.SetAddress ();
				p.user.SetAva ();
			}

			//set binding source
			listViewLenders.ItemsSource = lenders;
			stackPanelInfo.DataContext = selectedBook;

			ControlMethods.SwitchVisibility ( false , progressBar );
			relativePanelInfo.Visibility = Visibility.Visible;
			ControlMethods.SwitchVisibility ( true , listViewLenders );
		}

		private void SendRequest ( object sender , RoutedEventArgs e )
		{
			string postId = ( ( Button ) sender ).Tag.ToString ();
			SendRequestToPost ( postId , sender );
		}

		private async void SendRequestToPost ( string postId , object sender )
		{
			dynamic request = new
			{
				postId = postId ,
				userId = UserData.id
			};
			string result = await RestAPI.SendJson ( request , RestAPI.phpAddress , "SendRequest" );
			if ( JsonHelper.IsRequestSucceed ( result ) == RestAPI.ResponseStatus.OK )
			{
				CustomNotification.ShowNotification ( "Đã gửi yêu cầu" );
				( ( Button ) sender ).IsEnabled = false;
			}
			else
			{
				CustomNotification.ShowDialogMessage ();
			}
		}

		private async void AddToYourBook ( object sender , RoutedEventArgs e )
		{
			if ( ( ( Button ) sender ).Tag.ToString () == "1" )
			{
				//add book
				dynamic book = new
				{
					bookId = bookId ,
					userId = UserData.id
				};
				string addResult = await RestAPI.SendJson ( book , RestAPI.phpAddress , "AddToYourBook" );
				if ( JsonHelper.IsRequestSucceed ( addResult ) == RestAPI.ResponseStatus.OK )
				{
					( ( Button ) sender ).Content = "Xóa";
					( ( Button ) sender ).Tag = 0;
				}
				else
				{
					CustomNotification.ShowDialogMessage ();
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
				string addResult = await RestAPI.SendJson ( book , RestAPI.phpAddress , "RemoveFromYourBook" );
				if ( JsonHelper.IsRequestSucceed ( addResult ) == RestAPI.ResponseStatus.OK )
				{
					( ( Button ) sender ).Content = "Thêm";
					( ( Button ) sender ).Tag = 1;
				}
				else
				{
					CustomNotification.ShowDialogMessage ();
				}
			}
		}

		private void UserAccountTapped ( object sender , TappedRoutedEventArgs e )
		{
			string userId = ( ( TextBlock ) sender ).Tag.ToString ();
			Frame.Navigate ( typeof ( UserInfo ) , userId );
		}
	}
}
