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

			//hide posted book and requested book
			scrollViewerBooks.Visibility = Visibility.Visible;
			scrollViewerBooks2.Visibility = Visibility.Visible;
			//hide received request list and sent request list 
			scrollViewerReceivedRequest.Visibility = Visibility.Collapsed;
			scrollViewerSentRequest.Visibility = Visibility.Collapsed;
			//hide notification
			scrollViewerNotification.Visibility = Visibility.Visible;
			GetData ();
		}

		private List<PostedBook> bookUserPosted;
		private List<PostedBook> bookUserRequested;
		private ObservableCollection<Request> receivedRequest;
		private ObservableCollection<Post> requestedPost;

		private ObservableCollection<RequestNotification> requestNotifications;

		private async void GetData ()
		{
			//show progress bar and book view
			progressBar.Visibility = Visibility.Visible;
			//get books that user posted
			string r1 = await GetPostedBooks ();
			string r2 = await GetSentRequests ();
			string r3 = await GetRequestNotifications ();
			if ( r1 != null && r2 != null && r3 != null )
			{
				//hide progress bar
				progressBar.Visibility = Visibility.Collapsed;
			}
			else
			{
				progressBar.Visibility = Visibility.Collapsed;
				CustomNotification.ShowDialogMessage ();
			}
		}

		private async Task<string> GetRequestNotifications ()
		{
			string result = await RestAPI.SendJson ( UserData.id , RestAPI.phpAddress , "GetRequestNotifications" );
			dynamic json = JObject.Parse ( result );
			string status = json.status;
			if ( status == "200" )
			{
				//success
				requestNotifications = new ObservableCollection<RequestNotification> ();
				for ( int i = 0 ; i < json.notifications.Count ; i++ )
				{
					//create new request notification
					RequestNotification rn = new RequestNotification ()
					{
						requestId = json.notifications[i].requestId ,
						requestAccepted = ( json.notifications[i].requestAccepted == 1 ) ? true : false ,
						userAccount = json.notifications[i].userAccount ,
						bookId = json.notifications[i].bookId ,
						bookTitle = json.notifications[i].bookTitle
					};
					rn.SetContent ();
					requestNotifications.Add ( rn );
				}
				listViewNotification.ItemsSource = requestNotifications;
				return "OK";
			}
			else
			{
				return "";
			}
		}

		private async Task<string> GetSentRequests ()
		{
			string result = await RestAPI.SendJson ( UserData.id , RestAPI.phpAddress , "GetRequestsFromUser" );
			dynamic json = JObject.Parse ( result );
			string status = json.status;
			if ( status == "200" )
			{
				//success
				bookUserRequested = new List<PostedBook> ();
				for ( int i = 0 ; i < json.postedBooks.Count ; i++ )
				{
					//create new PostedBook
					PostedBook p = new PostedBook
					{
						bookId = json.postedBooks[i].bookId ,
						title = json.postedBooks[i].title ,
						image = RestAPI.serverAddress + "cover/" + json.postedBooks[i].bookId + ".jpg" ,
						author = json.postedBooks[i].author ,
					};
					//initialize request list
					List<Post> pl = new List<Post> ();
					for ( int j = 0 ; j < json.postedBooks[i].posts.Count ; j++ )
					{
						Post r = new Post
						{
							postId = json.postedBooks[i].posts[j].postId ,
							postUserId = json.postedBooks[i].posts[j].postUserId ,
							postUserAccount = json.postedBooks[i].posts[j].postUserAccount ,
							postUserAva = RestAPI.serverAddress + "resources/images/defaultAva.png"
						};
						pl.Add ( r );
					}
					p.posts = new ObservableCollection<Post> ( pl );
					bookUserRequested.Add ( p );
				}
				listViewBook2.ItemsSource = bookUserRequested;
				return "OK";
			}
			else if ( status == "204" )
			{
				return "Empty";
			}
			else
			{
				return "";
			}
		}

		protected override void OnNavigatedTo ( NavigationEventArgs e )
		{
			//
		}

		private async Task<string> GetPostedBooks ()
		{
			string result = await RestAPI.SendJson ( UserData.id , RestAPI.phpAddress , "GetRequestsForUser" );
			dynamic json = JObject.Parse ( result );
			string status = json.status;
			if ( status == "200" )
			{
				//success
				bookUserPosted = new List<PostedBook> ();
				for ( int i = 0 ; i < json.postedBooks.Count ; i++ )
				{
					//create new PostedBook
					PostedBook p = new PostedBook
					{
						postId = json.postedBooks[i].postId ,
						title = json.postedBooks[i].title ,
						image = RestAPI.serverAddress + "cover/" + json.postedBooks[i].bookId + ".jpg" ,
						author = json.postedBooks[i].author ,
					};
					//initialize request list
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
					p.requests = new ObservableCollection<Request> ( rl );
					bookUserPosted.Add ( p );
				}
				listViewBook.ItemsSource = bookUserPosted;
				return "OK";
			}
			else if ( status == "204" )
			{
				return "Empty";
			}
			else
			{
				return "";
			}
		}

		private void BookGridTapped ( object sender , TappedRoutedEventArgs e )
		{
			//user click on a book
			//get the tag contains postId
			string tag = ( ( Grid ) sender ).Tag.ToString ();

			//show requests
			//find request in book's request list
			receivedRequest = bookUserPosted.First ( p => p.postId == tag ).requests;
			listViewReceivedRequest.ItemsSource = receivedRequest;
			//hide posted books
			scrollViewerBooks.Visibility = Visibility.Collapsed;
			//show request list
			scrollViewerReceivedRequest.Visibility = Visibility.Visible;
		}

		private async void AcceptRequest ( object sender , RoutedEventArgs e )
		{
			string requestId = ( ( Button ) sender ).Tag.ToString ();
			string status = await SendUserResponse ( true , requestId );
			if ( status == "OK" )
			{
				//send response succeed
				//return request id to remove
				receivedRequest.Remove ( receivedRequest.First ( p => p.requestId == requestId ) );
			}
			else
			{
				//send response failed
				CustomNotification.ShowDialogMessage ();
			}
		}

		private async void DenyRequest ( object sender , RoutedEventArgs e )
		{
			string requestId = ( ( Button ) sender ).Tag.ToString ();
			string status = await SendUserResponse ( false , requestId );
			if ( status == "OK" )
			{
				//send response succeed
				//return request id to remove
				receivedRequest.Remove ( receivedRequest.First ( p => p.requestId == requestId ) );
			}
			else
			{
				//send response failed
				CustomNotification.ShowDialogMessage ();
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

		private void BackToPostedBookList ( object sender , RoutedEventArgs e )
		{
			//app is showing request list
			//user want to move back to posted books
			scrollViewerReceivedRequest.Visibility = Visibility.Collapsed;
			scrollViewerBooks.Visibility = Visibility.Visible;

			//app is showing post list
			//user want to move back to requested books
			scrollViewerSentRequest.Visibility = Visibility.Collapsed;
			scrollViewerBooks2.Visibility = Visibility.Visible;
		}

		private void RequestedBookGridTapped ( object sender , TappedRoutedEventArgs e )
		{
			//user click on a book
			//get the tag contains bookId
			string tag = ( ( Grid ) sender ).Tag.ToString ();

			//show posts
			//find post in book's request list
			requestedPost = bookUserRequested.First ( p => p.bookId == tag ).posts;
			listViewSentRequest.ItemsSource = requestedPost;
			//hide posted books
			scrollViewerBooks2.Visibility = Visibility.Collapsed;
			//show post list
			scrollViewerSentRequest.Visibility = Visibility.Visible;
		}

		private async void DeleteRequest ( object sender , RoutedEventArgs e )
		{
			string postId = ( ( Button ) sender ).Tag.ToString ();
			dynamic data = new
			{
				postId = postId ,
				userId = UserData.id
			};
			string result = await RestAPI.SendJson ( data , RestAPI.phpAddress , "DeleteRequestToPost" );
			dynamic json = JObject.Parse ( result );
			string status = json.status;
			if ( status == "200" )
			{
				//send response succeed
				//return request id to remove
				requestedPost.Remove ( requestedPost.First ( p => p.postId == postId ) );
			}
			else
			{
				//send response failed
				CustomNotification.ShowDialogMessage ();
			}
		}

		private void ToUser ( object sender , RoutedEventArgs e )
		{
			//move to user page
		}

		private async void DeactiveRequest ( object sender , RoutedEventArgs e )
		{
			//deactive request
			string requestId = ( ( Button ) sender ).Tag.ToString ();
			string result = await RestAPI.SendJson ( requestId , RestAPI.phpAddress , "DeactiveRequest" );
			dynamic json = JObject.Parse ( result );
			string status = json.status;
			if ( status == "200" )
			{
				//send response succeed
				//return request id to remove
				requestNotifications.Remove ( requestNotifications.First ( p => p.requestId == requestId ) );
			}
			else
			{
				//send response failed
				CustomNotification.ShowDialogMessage ();
			}
		}
	}
}
