using BookShare.Helper;
using BookShare.Model;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
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
			//show progress bar
			ControlMethods.SwitchVisibility ( true , progressBar );
			GetData ();

		}

		private ObservableCollection<PostedBook> postedBooks;
		private ObservableCollection<PostedBook> requestedBooks;
		private ObservableCollection<Request> receivedRequest;
		private ObservableCollection<Post> requestedPost;

		private async void GetData ()
		{
			try
			{
				await Task.WhenAll ( GetPostedBooks () , GetrequestedBooks () );
			}
			catch ( Exception e )
			{
				gridNotification.Show ( true );
			}

			//hide progress bar
			ControlMethods.SwitchVisibility ( false , progressBar );
			//show posted book and requested book
			ControlMethods.SwitchVisibility ( true , listViewPostedBook );
			ControlMethods.SwitchVisibility ( true , listViewRequestedBook );
		}

		private async Task GetPostedBooks ()
		{
			string result =
				await RestAPI.SendGetRequest ( RestAPI.publicApiAddress + "request/received/" + UserData.id );
			if ( JsonHelper.IsRequestSucceed ( result ) == RestAPI.ResponseStatus.OK )
			{
				string data = JsonHelper.DecodeJson ( result );
				postedBooks = JsonHelper.ConvertToPostedBooks ( data );
				if ( postedBooks != null && postedBooks.Count > 0 )
					foreach ( PostedBook pt in postedBooks )
					{
						pt.SetImageLink ();
					}
				listViewPostedBook.ItemsSource = postedBooks;
			}
			else if ( JsonHelper.IsRequestSucceed ( result ) == RestAPI.ResponseStatus.Failed )
				throw new Exception ();
		}

		private async Task GetrequestedBooks ()
		{
			string result =
				await RestAPI.SendGetRequest ( RestAPI.publicApiAddress + "request/sent/" + UserData.id );
			if ( JsonHelper.IsRequestSucceed ( result ) == RestAPI.ResponseStatus.OK )
			{
				string data = JsonHelper.DecodeJson ( result );
				requestedBooks = JsonHelper.ConvertToPostedBooks ( data );
				if ( requestedBooks != null && requestedBooks.Count > 0 )
					foreach ( PostedBook pt in requestedBooks )
					{
						pt.SetImageLink ();
					}
				listViewRequestedBook.ItemsSource = requestedBooks;
			}
			else if ( JsonHelper.IsRequestSucceed ( result ) == RestAPI.ResponseStatus.Failed )
				throw new Exception ();
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
			ControlMethods.SwitchVisibility ( true , progressBar );
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
				gridNotification.Show ( true );
			}
			ControlMethods.SwitchVisibility ( false , progressBar );
		}

		private async void DenyRequest ( object sender , RoutedEventArgs e )
		{
			ControlMethods.SwitchVisibility ( true , progressBar );
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
				gridNotification.Show ( true );
			}
			ControlMethods.SwitchVisibility ( false , progressBar );
		}

		private async Task<RestAPI.ResponseStatus> SendUserResponse ( bool isAccepted , string requestId )
		{
			dynamic data = new
			{
				requestId = requestId ,
				isAccepted = isAccepted
			};
			string result = await RestAPI.SendPostRequest ( data , RestAPI.publicApiAddress + "request/respond/" );
			if ( JsonHelper.IsRequestSucceed ( result ) == RestAPI.ResponseStatus.OK )
			{
				return RestAPI.ResponseStatus.OK;
			}
			return RestAPI.ResponseStatus.Failed;
		}

		private async void DeleteRequest ( object sender , RoutedEventArgs e )
		{
			ControlMethods.SwitchVisibility ( true , progressBar );
			string postId = ( ( Button ) sender ).Tag.ToString ();
			dynamic data = new
			{
				postId = postId ,
				userId = UserData.id
			};
			string result = await RestAPI.SendPostRequest ( data , RestAPI.publicApiAddress + "request/delete/" );
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
				gridNotification.Show ( true );
			}
			ControlMethods.SwitchVisibility ( false , progressBar );
		}

		private void TextBlockUserTapped ( object sender , TappedRoutedEventArgs e )
		{
			string userId = ( ( TextBlock ) sender ).Tag.ToString ();
			NavigateToUser ( userId );
		}

		private void NavigateToUser ( string userId )
		{
			Frame.Navigate ( typeof ( UserInfo ) , userId );
		}
	}
}
