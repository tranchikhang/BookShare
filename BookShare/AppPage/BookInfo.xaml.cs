using BookShare.Helper;
using BookShare.Model;
using Newtonsoft.Json;
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

		protected override void OnNavigatedTo ( NavigationEventArgs e )
		{
			var id = e.Parameter as String;
			LoadBookInfo ( id );
		}

		private async void LoadBookInfo ( string id )
		{
			Book book = new Book ();
			Author author = new Author ();
			string result = await RestAPI.SendJson ( id , RestAPI.phpAdress + "client/book/getbook.php" , "GetBookById" );
			Dictionary<string , object> d = JsonConvert.DeserializeObject<Dictionary<string , object>> ( result );
			book = JsonConvert.DeserializeObject<Book> ( d["book"].ToString () );
			author = JsonConvert.DeserializeObject<Author> ( d["author"].ToString () );
			book.SetImageLink ();
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
