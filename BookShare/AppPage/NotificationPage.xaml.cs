using BookShare.Helper;
using BookShare.Model;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace BookShare.AppPage
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class NotificationPage : Page
	{
		public NotificationPage ()
		{
			this.InitializeComponent ();
			LoadNotification ();
		}

		protected override void OnNavigatedTo ( NavigationEventArgs e )
		{
			NavigationMethod.SetBackButtonVisibility ( false );
		}

		private ObservableCollection<RequestNotification> requestNotifications;

		private async void LoadNotification ()
		{
			await GetRequestNotifications ();
			ControlMethods.SwitchVisibility ( false , progressBar );
			ControlMethods.SwitchVisibility ( true , listViewNotification );
		}

		private async Task GetRequestNotifications ()
		{
			string result =
				await RestAPI.SendGetRequest ( RestAPI.publicApiAddress + "request/notification/" + UserData.id );
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
			else if ( JsonHelper.IsRequestSucceed ( result ) == RestAPI.ResponseStatus.Failed )
				throw new Exception ();
		}

		private void ButtonUserTapped ( object sender , RoutedEventArgs e )
		{
			string userId = ( ( Button ) sender ).Tag.ToString ();
			NavigateToUser ( userId );
		}

		private async void DeactiveRequest ( object sender , RoutedEventArgs e )
		{
			ControlMethods.SwitchVisibility ( true , progressBar );
			//deactive request
			string requestId = ( ( Button ) sender ).Tag.ToString ();
			string result = await RestAPI.SendPostRequest ( requestId , RestAPI.publicApiAddress + "request/deactivate/" );
			if ( JsonHelper.IsRequestSucceed ( result ) == RestAPI.ResponseStatus.OK )
			{
				//send response succeed
				//return request id to remove
				requestNotifications.Remove ( requestNotifications.First ( p => p.requestId == requestId ) );
			}
			else
			{
				//send response failed
				gridNotification.Show ( true );
			}
			ControlMethods.SwitchVisibility ( false , progressBar );
		}

		private void NavigateToUser ( string userId )
		{
			Frame.Navigate ( typeof ( UserInfo ) , userId );
		}
	}
}
