using BookShare.Helper;
using BookShare.Model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
	public sealed partial class RequestList : Page
	{
		public RequestList ()
		{
			this.InitializeComponent ();
			progressBar.Visibility = Visibility.Visible;
			listViewBook.Visibility = Visibility.Collapsed;
			listViewRequest.Visibility = Visibility.Collapsed;
		}

		protected override void OnNavigatedTo ( NavigationEventArgs e )
		{
			GetPostedBooks ();
		}

		private List<PostedBook> postedBooks;
		private List<Request> requestList;

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
			progressBar.Visibility = Visibility.Collapsed;
			listViewBook.Visibility = Visibility.Visible;
		}

		private void BookGridTapped ( object sender , TappedRoutedEventArgs e )
		{
			string tag = ( ( Grid ) sender ).Tag.ToString ();
			listViewRequest.ItemsSource = postedBooks.First ( p => p.postId == tag ).requests;
			listViewRequest.Visibility = Visibility.Visible;
		}
	}
}
