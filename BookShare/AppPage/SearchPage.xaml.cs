using BookShare.Helper;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml;
using System.Collections.ObjectModel;
using BookShare.Model;
using System.Threading.Tasks;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace BookShare.AppPage
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class SearchPage : Page
	{
		public SearchPage ()
		{
			InitializeComponent ();
		}

		protected override async void OnNavigatedTo ( NavigationEventArgs e )
		{
			query = ( string ) e.Parameter;
			ControlMethods.SwitchVisibility ( true , progressBar );
			SearchBox.Text = query;
			await SendSearchQuery ( query );
			ControlMethods.SwitchVisibility ( false , progressBar );
		}

		protected override void OnNavigatedFrom ( NavigationEventArgs e )
		{
			//
		}

		public string query;
		private ObservableCollection<Book> books;

		private async Task SendSearchQuery ( string query )
		{
			string result = await RestAPI.SendJson ( query , RestAPI.phpAddress , "GetSearchResult" );
			if ( JsonHelper.IsRequestSucceed ( result ) == RestAPI.ResponseStatus.Empty )
			{
				//no book to display
				//show option to add new book
				ControlMethods.SwitchVisibility ( false , listViewResults );
				stackPanelAddNew.Visibility = Visibility.Visible;
			}
			else if ( JsonHelper.IsRequestSucceed ( result ) == RestAPI.ResponseStatus.OK )
			{
				//result is not empty
				string data = JsonHelper.DecodeJson ( result );
				books = JsonHelper.ConvertToBooks ( data );
				foreach ( Book b in books )
				{
					b.SetImageLink ();
				}
				listViewResults.ItemsSource = books;
				stackPanelAddNew.Visibility = Visibility.Collapsed;
				ControlMethods.SwitchVisibility ( true , listViewResults );
			}
			else
				gridNotification.Show ( true );
		}

		private void TitleTapped ( object sender , TappedRoutedEventArgs e )
		{
			string value = ( ( TextBlock ) sender ).Tag.ToString ();
			Frame.Navigate ( typeof ( BookInfo ) , value );
		}

		private void AddNewBook ( object sender , RoutedEventArgs e )
		{
			Frame.Navigate ( typeof ( AddNewBook ) );
		}

		private async void SearchClick ( object sender , RoutedEventArgs e )
		{
			stackPanelAddNew.Visibility = Visibility.Collapsed;
			ControlMethods.SwitchVisibility ( true , progressBar );
			await SendSearchQuery ( SearchBox.Text );
			ControlMethods.SwitchVisibility ( false , progressBar );
		}
	}
}
