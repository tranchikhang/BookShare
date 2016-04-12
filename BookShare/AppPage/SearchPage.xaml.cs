using BookShare.Helper;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml;
using System.Collections.ObjectModel;
using BookShare.Model;
using System.Threading.Tasks;
using System;
using Windows.Storage;

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
			ControlMethods.SwitchVisibility ( true , progressBar );
			if ( e.Parameter != null )
			{
				query = ( string ) e.Parameter;
				SearchBox.Text = query;
				await SendSearchQuery ( query );
			}
			else if ( ApplicationData.Current.LocalSettings.Values["q"] != null
				&& e.NavigationMode == NavigationMode.Back )
			{
				query = ( string ) ApplicationData.Current.LocalSettings.Values["q"];
				ApplicationData.Current.LocalSettings.Values.Remove ( "q" );
				SearchBox.Text = query;
				await SendSearchQuery ( query );
			}
			await GetLocation ();
			ControlMethods.SwitchVisibility ( false , progressBar );
		}

		private ObservableCollection<City> cities;

		public async Task GetLocation ()
		{
			string result = await RestAPI.SendGetRequest ( RestAPI.publicApiAddress + "location/" );
			if ( JsonHelper.IsRequestSucceed ( result ) == RestAPI.ResponseStatus.OK )
			{
				string data = JsonHelper.DecodeJson ( result );
				//deserialize all location into list
				cities = JsonHelper.ConvertToCities ( data );
				foreach ( City c in cities )
				{
					c.districts.Insert ( 0 , new District
					{
						id = "" ,
						name = ""
					} );
				}
				cities.Insert ( 0 , new City
				{
					id = "" ,
					name = ""
				} );
				comboCity.ItemsSource = cities;
			}
		}

		protected override void OnNavigatedFrom ( NavigationEventArgs e )
		{
		}

		public string query;
		private ObservableCollection<Book> books;

		private async Task SendSearchQuery ( string query )
		{
			string queryString = "q=" + System.Net.WebUtility.UrlEncode ( query );
			if ( comboCity.SelectedValue != null )
			{
				queryString += "&city=" + comboCity.SelectedValue;
			}
			if ( comboDistrict.SelectedValue != null )
			{
				queryString += "&district=" + comboDistrict.SelectedValue;
			}
			string result = await RestAPI.SendGetRequest ( RestAPI.publicApiAddress + "book/search/?" + queryString );
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
			string value = ( ( Grid ) sender ).Tag.ToString ();
			//save query
			ApplicationData.Current.LocalSettings.Values["q"] = SearchBox.Text;
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

		private void comboCity_SelectionChanged ( object sender , SelectionChangedEventArgs e )
		{
			if ( comboCity.SelectedValue != null )
			{
				comboDistrict.ItemsSource = ( ( City ) comboCity.SelectedItem ).districts;
			}
		}
	}
}
