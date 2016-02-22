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
	public sealed partial class BookShelf : Page
	{
		public BookShelf ()
		{
			this.InitializeComponent ();
		}

		protected override void OnNavigatedTo ( NavigationEventArgs e )
		{
			GetPostedBooks ();
		}

		private List<object> postedBooks;

		private async void GetPostedBooks ()
		{
			string result = await RestAPI.SendJson ( UserData.id , RestAPI.phpAddress , "GetPostedBooks" );
			dynamic json = JObject.Parse ( result );
			string status = json.status;
			string message = json.message;
			if ( status == "200" )
			{
				postedBooks = new List<object> ();
				for ( int i = 0 ; i < json.postedBooks.Count ; i++ )
				{
					postedBooks.Add ( new
					{
						postId = json.postedBooks[i].postId ,
						isAvailable = json.postedBooks[i].available ,
						title = json.postedBooks[i].title ,
						image = RestAPI.serverAddress + "cover/" + json.postedBooks[i].bookId + ".jpg" ,
						author = json.postedBooks[i].author
					} );
				}
				listBoxPostedBooks.ItemsSource = postedBooks;
			}
		}
	}
}
