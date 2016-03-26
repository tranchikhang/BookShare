using BookShare.Helper;
using BookShare.Model;
using System.Collections.ObjectModel;
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
	public sealed partial class GreetingPage : Page
	{
		public GreetingPage ()
		{
			this.InitializeComponent ();
		}

		private int numberOfRandomBooks = 4;
		private int numberOfNewestBooks = 4;

		protected override void OnNavigatedTo ( NavigationEventArgs e )
		{
			NavigationMethod.SetBackButtonVisibility ( false );
			//get random books
			GetRandomBooks ( numberOfRandomBooks );
			//get newest books
		}

		ObservableCollection<Book> randomBooks;

		private async void GetRandomBooks ( int numberOfBooks )
		{
			string result = await RestAPI.SendJson ( numberOfBooks , RestAPI.phpAddress , "GetRandomBooks" );
			if ( JsonHelper.IsRequestSucceed ( result ) == RestAPI.ResponseStatus.OK )
			{
				string data = JsonHelper.DecodeJson ( result );
				randomBooks = JsonHelper.ConvertToBooks ( data );
				foreach ( Book b in randomBooks )
				{
					b.SetImageLink ();
				}
				listViewBooks.ItemsSource = randomBooks;
			}
			else
				gridNotification.Show ( true );
		}

		private void TitleTapped ( object sender , TappedRoutedEventArgs e )
		{
			string value = ( ( TextBlock ) sender ).Tag.ToString ();
			Frame.Navigate ( typeof ( BookInfo ) , value );
		}

		private void SearchClick ( object sender , RoutedEventArgs e )
		{
			string query = SearchBox.Text;
			Frame.Navigate ( typeof ( SearchPage ) , query );
		}
	}
}
