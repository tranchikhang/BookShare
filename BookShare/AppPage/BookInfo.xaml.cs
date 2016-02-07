﻿using BookShare.Helper;
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
		//dynamic lenders;
		private List<object> lenders;
		protected override void OnNavigatedTo ( NavigationEventArgs e )
		{
			bookId = e.Parameter as String;
			LoadBookInfo ( bookId );
			DisplayBackButton ();
		}

		private void DisplayBackButton ()
		{
			Frame rootFrame = ( ( App ) Application.Current ).AppSplitView.Content as Frame;

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

		private async void LoadBookInfo ( string id )
		{
			Book book = new Book ();
			Author author = new Author ();

			//send request and get response
			string bookInfo = await RestAPI.SendJson ( id , RestAPI.phpAdress + "client/book/getbook.php" , "GetBookById" );
			string bookLenders = await RestAPI.SendJson ( id , RestAPI.phpAdress + "client/book/getbook.php" , "GetLenderForBook" );

			//deserialize into dictionary
			Dictionary<string , object> d = JsonConvert.DeserializeObject<Dictionary<string , object>> ( bookInfo );
			book = JsonConvert.DeserializeObject<Book> ( d["book"].ToString () );
			author = JsonConvert.DeserializeObject<Author> ( d["author"].ToString () );

			book.SetImageLink ();

			if ( bookLenders != "empty" )
			{
				dynamic json = JArray.Parse ( bookLenders );
				lenders = new List<object> ();
				for ( int i = 0 ; i < json.Count ; i++ )
				{
					lenders.Add ( new
					{
						postid = json[i].postid,
						userid = json[i].userid,
						account = json[i].account ,
						address = json[i].district + ", " + json[i].city,
						userAva = RestAPI.serverAdress + "resources/images/defaultAva.png"
					} );
				}
				listLenders.ItemsSource = lenders;
			}

			DisplayBookInfo ( book , author );
		}

		private void DisplayBookInfo ( Book book , Author author )
		{
			BookCover.Source = new BitmapImage ( new Uri ( @book.image ) );
			tblkTitle.Text = book.title;
			tblkAuthor.Text = author.name;
			tblkYear.Text = book.year;
			tblkDes.Text = book.description;
		}
	}
}
