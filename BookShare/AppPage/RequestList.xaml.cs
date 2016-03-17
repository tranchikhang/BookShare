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
using Windows.UI.Core;
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
			ControlMethods.SwitchVisibility ( false , listViewPostedBook );
			ControlMethods.SwitchVisibility ( false , listViewRequestedBook );
			//hide received request list and requested posts
			ControlMethods.SwitchVisibility ( false , listViewReceivedRequest );
			ControlMethods.SwitchVisibility ( false , listViewSentRequest );
			//hide notification
			ControlMethods.SwitchVisibility ( false , listViewNotification );
			GetData ();
		}

		private ObservableCollection<PostedBook> postedBooks;
		private ObservableCollection<PostedBook> requestedBooks;
		private ObservableCollection<Request> receivedRequest;
		private ObservableCollection<Post> requestedPost;
		private ObservableCollection<RequestNotification> requestNotifications;

		private void GetData ()
		{
			//show progress bar
			ControlMethods.SwitchVisibility ( true , progressBar );
			//get books that user posted
			GetPostedBooks ();
			GetrequestedBooks ();
			GetRequestNotifications ();
			//hide progress bar
			progressBar.Visibility = Visibility.Collapsed;
			ControlMethods.SwitchVisibility ( true , listViewPostedBook );
			ControlMethods.SwitchVisibility ( true , listViewRequestedBook );
			ControlMethods.SwitchVisibility ( true , listViewNotification );
		}

		private async void GetPostedBooks ()
		{
			string result = await RestAPI.SendJson ( UserData.id , RestAPI.phpAddress , "GetRequestsForUser" );
			if ( JsonHelper.IsRequestSucceed ( result ) == RestAPI.ResponseStatus.OK )
			{
				string data = JsonHelper.DecodeJson ( result );
				postedBooks = JsonHelper.ConvertToPostedBooks ( data );
				if ( postedBooks != null && postedBooks.Count > 0 )
					foreach ( PostedBook pt in postedBooks )
					{
						pt.SetImageLink ();
						if ( pt.requests != null && pt.requests.Count > 0 )
							foreach ( Request r in pt.requests )
							{
								r.user.SetAddress ();
								r.user.SetAva ();
							}
					}
				listViewPostedBook.ItemsSource = postedBooks;
			}
		}

		private async void GetRequestNotifications ()
		{
			string result = await RestAPI.SendJson ( UserData.id , RestAPI.phpAddress , "GetRequestNotifications" );
			if ( JsonHelper.IsRequestSucceed ( result ) == RestAPI.ResponseStatus.OK )
			{
				string data = JsonHelper.DecodeJson ( result );
				requestNotifications = JsonHelper.ConvertToRequestNotifications ( data );
				if ( requestNotifications != null && requestNotifications.Count > 0 )
					foreach ( RequestNotification rq in requestNotifications )
					{
						rq.SetContent ();
					}
				listViewNotification.ItemsSource = requestNotifications;
			}
		}

		private async void GetrequestedBooks ()
		{
			string result = await RestAPI.SendJson ( UserData.id , RestAPI.phpAddress , "GetRequestsFromUser" );
			if ( JsonHelper.IsRequestSucceed ( result ) == RestAPI.ResponseStatus.OK )
			{
				string data = JsonHelper.DecodeJson ( result );
				requestedBooks = JsonHelper.ConvertToPostedBooks ( data );
				if ( requestedBooks != null && requestedBooks.Count > 0 )
					foreach ( PostedBook pt in requestedBooks )
					{
						pt.SetImageLink ();
						if ( pt.posts != null && pt.posts.Count > 0 )
							foreach ( Post p in pt.posts )
							{
								p.user.SetAddress ();
								p.user.SetAva ();
							}
					}
				listViewRequestedBook.ItemsSource = requestedBooks;
			}
		}

		protected override void OnNavigatedTo ( NavigationEventArgs e )
		{
			//
		}

		private void PostedBookTapped ( object sender , TappedRoutedEventArgs e )
		{
			//user clicked on a book
			//get the tag contains postId
			string tag = ( ( Grid ) sender ).Tag.ToString ();

			//find all requests in post
			receivedRequest = postedBooks.First ( p => p.postId == tag ).requests;
			listViewReceivedRequest.ItemsSource = receivedRequest;
			//hide posted books
			ControlMethods.SwitchVisibility ( false , listViewPostedBook );
			//show request list
			ControlMethods.SwitchVisibility ( true , listViewReceivedRequest );

			HandleBackButton ();
		}


		private void RequestedBookTapped ( object sender , TappedRoutedEventArgs e )
		{
			//user clicked on a book
			//get the tag contains bookId
			string tag = ( ( Grid ) sender ).Tag.ToString ();

			//find all post with bookId
			requestedPost = requestedBooks.First ( p => p.bookId == tag ).posts;
			listViewSentRequest.ItemsSource = requestedPost;
			//hide posted books
			ControlMethods.SwitchVisibility ( false , listViewRequestedBook );
			//show post list
			ControlMethods.SwitchVisibility ( true , listViewSentRequest );

			HandleBackButton ();
		}
		private void HandleBackButton ()
		{
			//show back button
			SystemNavigationManager.GetForCurrentView ().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
			//back button event
			SystemNavigationManager.GetForCurrentView ().BackRequested += BackButtonClick;
		}

		private void BackButtonClick ( object sender , BackRequestedEventArgs e )
		{
			//app is showing request list
			//user want to move back to posted books
			ControlMethods.SwitchVisibility ( true , listViewPostedBook );
			ControlMethods.SwitchVisibility ( false , listViewReceivedRequest );
			//app is showing post list
			//user want to move back to requested books
			ControlMethods.SwitchVisibility ( true , listViewRequestedBook );
			ControlMethods.SwitchVisibility ( false , listViewSentRequest );
			SystemNavigationManager.GetForCurrentView ().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Collapsed;
		}

		private async void AcceptRequest ( object sender , RoutedEventArgs e )
		{
			string requestId = ( ( Button ) sender ).Tag.ToString ();
			RestAPI.ResponseStatus status = await SendUserResponse ( true , requestId );
			if ( status == RestAPI.ResponseStatus.OK )
			{
				//send response succeed
				//return request id to remove
				receivedRequest.Remove ( receivedRequest.First ( p => p.id == requestId ) );
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
			RestAPI.ResponseStatus status = await SendUserResponse ( false , requestId );
			if ( status == RestAPI.ResponseStatus.OK )
			{
				//send response succeed
				//return request id to remove
				receivedRequest.Remove ( receivedRequest.First ( p => p.id == requestId ) );
			}
			else
			{
				//send response failed
				CustomNotification.ShowDialogMessage ();
			}
		}

		private async Task<RestAPI.ResponseStatus> SendUserResponse ( bool isAccepted , string requestId )
		{
			dynamic data = new
			{
				requestId = requestId ,
				isAccepted = isAccepted
			};
			string result = await RestAPI.SendJson ( data , RestAPI.phpAddress , "RespondToRequest" );
			if ( JsonHelper.IsRequestSucceed ( result ) == RestAPI.ResponseStatus.OK )
			{
				return RestAPI.ResponseStatus.OK;
			}
			return RestAPI.ResponseStatus.Failed;
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
			if ( JsonHelper.IsRequestSucceed ( result ) == RestAPI.ResponseStatus.OK )
			{
				//send response succeed
				//remove request
				requestedPost.Remove ( requestedPost.First ( p => p.id == postId ) );
				//check if posts in book is empty
				//if true, remove the book
				requestedBooks.Remove ( requestedBooks.First ( r => r.posts.Count == 0 ) );
			}
			else
			{
				//send response failed
				CustomNotification.ShowDialogMessage ();
			}
		}

		private void ToUser ( object sender , RoutedEventArgs e )
		{
			string userId = ( ( Button ) sender ).Tag.ToString ();
			//show back button
			SystemNavigationManager.GetForCurrentView ().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
			//back button event
			SystemNavigationManager.GetForCurrentView ().BackRequested += BackButtonClickToRquestPage;
			Frame.Navigate ( typeof ( UserInfo ) , userId );
		}

		private void BackButtonClickToRquestPage ( object sender , BackRequestedEventArgs e )
		{
			if ( Frame.CanGoBack )
				Frame.GoBack ();
			SystemNavigationManager.GetForCurrentView ().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Collapsed;
		}

		private async void DeactiveRequest ( object sender , RoutedEventArgs e )
		{
			//deactive request
			string requestId = ( ( Button ) sender ).Tag.ToString ();
			string result = await RestAPI.SendJson ( requestId , RestAPI.phpAddress , "DeactiveRequest" );
			if ( JsonHelper.IsRequestSucceed ( result ) == RestAPI.ResponseStatus.OK )
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
