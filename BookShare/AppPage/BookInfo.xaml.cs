using BookShare.Helper;
using BookShare.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
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
		}

		private string bookId;
		private List<object> lenders;
		protected override void OnNavigatedTo ( NavigationEventArgs e )
		{
			bookId = e.Parameter as String;
			LoadBookInfo ();
			DisplayBackButton ();
		}

		private void DisplayBackButton ()
		{
			//Frame rootFrame = ( ( App ) Application.Current ).AppSplitView.Content as Frame;
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
			Book book = new Book ();
			Author author = new Author ();

			//send request with book id and user id
			dynamic data = new
			{
				bookId = bookId ,
				userId = UserData.id
			};
			string bookInfo = await RestAPI.SendJson ( data , RestAPI.phpAddress , "GetBookById" );

			dynamic getLenders = new
			{
				bookId = bookId ,
				userId = UserData.id
			};
			string bookLenders = await RestAPI.SendJson ( getLenders , RestAPI.phpAddress , "GetLenderForBook" );

			//deserialize into dictionary
			Dictionary<string , object> d = JsonConvert.DeserializeObject<Dictionary<string , object>> ( bookInfo );
			book = JsonConvert.DeserializeObject<Book> ( d["book"].ToString () );
			author = JsonConvert.DeserializeObject<Author> ( d["author"].ToString () );
			bool isBookAdded = ( bool ) d["isBookAdded"];

			book.SetImageLink ();

			if ( bookLenders != "empty" )
			{
				dynamic json = JArray.Parse ( bookLenders );
				lenders = new List<object> ();
				for ( int i = 0 ; i < json.Count ; i++ )
				{
					lenders.Add ( new
					{
						postid = json[i].postId ,
						userid = json[i].userId ,
						account = json[i].account ,
						address = json[i].district + " - " + json[i].city ,
						userAva = RestAPI.serverAddress + "resources/images/defaultAva.png" ,
						isSent = ( json[i].isRequestSent == null ) ? true : false
					} );
				}
				listLenders.ItemsSource = lenders;
			}

			DisplayBookInfo ( book , author , isBookAdded );
		}

		private void DisplayBookInfo ( Book book , Author author , bool isBookAdded )
		{
			BookCover.Source = new BitmapImage ( new Uri ( @book.image ) );
			tblkTitle.Text = book.title;
			tblkAuthor.Text = author.name;
			tblkYear.Text = book.year;
			tblkDes.Text = book.description;
			if ( isBookAdded )
			{
				buttonAddBook.Content = "Xóa";
				buttonAddBook.Tag = 0;
			}
			else
			{
				buttonAddBook.Content = "Thêm";
				buttonAddBook.Tag = 1;
			}
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
			string sendResult = await RestAPI.SendJson ( request , RestAPI.phpAddress , "SendRequest" );
			dynamic json = JObject.Parse ( sendResult );
			string status = json.status;
			string message = json.message;
			if ( status == "200" )
			{
				MessageDialog dialog = new MessageDialog ( message );
				await dialog.ShowAsync ();
				( ( Button ) sender ).IsEnabled = false;
			}
			else
			{
				MessageDialog dialog = new MessageDialog ( "Có lỗi xảy ra, thử lại sau" );
				await dialog.ShowAsync ();
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
				dynamic json = JObject.Parse ( addResult );
				string status = json.status;
				string message = json.message;
				if ( status != "200" )
				{
					MessageDialog dialog = new MessageDialog ( "Có lỗi xảy ra, thử lại sau" );
					await dialog.ShowAsync ();
				}
				else
				{
					( ( Button ) sender ).Content = "Xóa";
					( ( Button ) sender ).Tag = 0;
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
				dynamic json = JObject.Parse ( addResult );
				string status = json.status;
				string message = json.message;
				if ( status != "200" )
				{
					MessageDialog dialog = new MessageDialog ( "Có lỗi xảy ra, thử lại sau" );
					await dialog.ShowAsync ();
				}
				else
				{
					( ( Button ) sender ).Content = "Thêm";
					( ( Button ) sender ).Tag = 1;
				}
			}
		}
	}
}
