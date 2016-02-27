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
	public sealed partial class RequestList : Page
	{
		public RequestList ()
		{
			this.InitializeComponent ();
			scrollViewerBooks.Visibility = Visibility.Collapsed;
		}

		protected override void OnNavigatedTo ( NavigationEventArgs e )
		{
			messageDialog = new MessageDialog ( "" );
		}

		private List<PostedBook> postedBooks;
		private MessageDialog messageDialog;

		private async void GetPostedBooks ()
		{
			string result = await RestAPI.SendJson ( UserData.id , RestAPI.phpAddress , "GetRequestsForUser" );
			dynamic json = JObject.Parse ( result );
			string status = json.status;
			if ( status == "200" )
			{
				postedBooks = new List<PostedBook> ();
				for ( int i = 0 ; i < json.postedBooks.Count ; i++ )
				{
					PostedBook p = new PostedBook
					{
						postId = json.postedBooks[i].postId ,
						title = json.postedBooks[i].title ,
						image = RestAPI.serverAddress + "cover/" + json.postedBooks[i].bookId + ".jpg" ,
						author = json.postedBooks[i].author ,
					};
					List<Request> rl = new List<Request> ();
					for ( int j = 0 ; j < json.postedBooks[i].requests.Count ; j++ )
					{
						Request r = new Request
						{
							requestId = json.postedBooks[i].requests[j].requestId ,
							requestUserAccount = json.postedBooks[i].requests[j].requestUserAccount ,
							requestUserAva = RestAPI.serverAddress + "resources/images/defaultAva.png" ,
							requestUserId = json.postedBooks[i].requests[j].requestUserId ,
							requestMessage = json.postedBooks[i].requests[j].requestMessage
						};
						rl.Add ( r );
					}
					p.requests = rl;
					postedBooks.Add ( p );
				}
				listViewBook.ItemsSource = postedBooks;
			}
			else
			{
				//204
			}
			//hide progress bar
			progressBar.Visibility = Visibility.Collapsed;
		}

		private void BookGridTapped ( object sender , TappedRoutedEventArgs e )
		{
			//user click on a book
			//get the tag contains postId
			string tag = ( ( Grid ) sender ).Tag.ToString ();

			//show requests
			listViewReceivedRequest.ItemsSource = postedBooks.First ( p => p.postId == tag ).requests;
			scrollViewerBooks.Visibility = Visibility.Collapsed;
			scrollViewerReceivedRequest.Visibility = Visibility.Visible;
		}

		private async void AcceptRequest ( object sender , RoutedEventArgs e )
		{
			progressBar.Visibility = Visibility.Visible;
			string requestId = ( ( Button ) sender ).Tag.ToString ();
			string status = await SendUserResponse ( true , requestId );
			if ( status == "OK" )
			{
				//send response succeed
				progressBar.Visibility = Visibility.Collapsed;
			}
			else
			{
				//send response failed
				progressBar.Visibility = Visibility.Collapsed;
				messageDialog.Title = "Lỗi";
				messageDialog.Content = "Đã xảy ra lỗi, thử lại sau";
				await messageDialog.ShowAsync ();
			}
		}

		private async void DenyRequest ( object sender , RoutedEventArgs e )
		{
			progressBar.Visibility = Visibility.Visible;
			string requestId = ( ( Button ) sender ).Tag.ToString ();
			string status = await SendUserResponse ( false , requestId );
			if ( status == "OK" )
			{
				//send response succeed
				progressBar.Visibility = Visibility.Collapsed;
			}
			else
			{
				//send response failed
				progressBar.Visibility = Visibility.Collapsed;
				messageDialog.Title = "Lỗi";
				messageDialog.Content = "Đã xảy ra lỗi, thử lại sau";
				await messageDialog.ShowAsync ();
			}
		}

		private async Task<string> SendUserResponse ( bool isAccepted , string requestId )
		{
			dynamic data = new
			{
				requestId = requestId ,
				isAccepted = isAccepted
			};
			string result = await RestAPI.SendJson ( data , RestAPI.phpAddress , "RespondToRequest" );
			dynamic json = JObject.Parse ( result );
			string status = json.status;
			if ( status == "200" )
			{
				string mes = json.message;
				return "OK";
			}
			return "ERROR";
		}

		private void ToReceivedRequest ( object sender , TappedRoutedEventArgs e )
		{
			//display list of book that user posted
			//show progress bar and book view
			scrollViewerBooks.Visibility = Visibility.Visible;
			progressBar.Visibility = Visibility.Visible;
			//hide the request type
			listviewRequestType.Visibility = Visibility.Collapsed;
			//get all books user posted
			GetPostedBooks ();
		}

		private void ToSentRequest ( object sender , TappedRoutedEventArgs e )
		{
			listviewRequestType.Visibility = Visibility.Collapsed;
		}

		private void BookListBack ( object sender , RoutedEventArgs e )
		{
			//user is on book list and press back
			//hide book list and show request type
			scrollViewerBooks.Visibility = Visibility.Collapsed;
			listviewRequestType.Visibility = Visibility.Visible;
		}

		private void ReceivedRequestBack ( object sender , RoutedEventArgs e )
		{
			//user is on list of received request
			//hide request list and show book list
			scrollViewerReceivedRequest.Visibility = Visibility.Collapsed;
			scrollViewerBooks.Visibility = Visibility.Visible;
		}
	}
}
