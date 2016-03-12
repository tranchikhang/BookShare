using BookShare.Helper;
using BookShare.Model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
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
	public sealed partial class UserInfo : Page
	{
		public UserInfo ()
		{
			this.InitializeComponent ();
			stackPanel.Visibility = Visibility.Visible;
			gridSendMessage.Visibility = Visibility.Collapsed;
		}

		private async void LoadData ( string userId )
		{
			string r = await GetUserInfo ( userId );
			if ( r == "OK" )
			{
				DisplayData ();
			}
			else
			{
				CustomNotification.ShowDialogMessage ();
			}
		}

		protected override void OnNavigatedTo ( NavigationEventArgs e )
		{
			string userId = e.Parameter as String;
			LoadData ( userId );
		}

		private void DisplayData ()
		{
			this.DataContext = user;
			if ( user.address == null )
				user.fullAddress = "Quận " + user.district + ", " + user.city;
			else
				user.fullAddress = user.address + ", quận " + user.district + ", " + user.city;
		}

		private User user;

		private async Task<string> GetUserInfo ( string userId )
		{
			string result = await RestAPI.SendJson ( userId , RestAPI.phpAddress , "GetUserInfo" );
			try
			{
				string data = JsonHelper.DecodeJson ( result );
				user = JsonHelper.ConvertToUser ( data );
				user.SetAva ();
				return "OK";
			}
			catch ( Exception ex )
			{
				CustomNotification.ShowDialogMessage ( content: ex.Message );
				return "";
			}
		}

		private void SendTap ( object sender , TappedRoutedEventArgs e )
		{
			//
			stackPanel.Visibility = Visibility.Collapsed;
			gridSendMessage.Visibility = Visibility.Visible;
		}

		private void BackClick ( object sender , RoutedEventArgs e )
		{
			stackPanel.Visibility = Visibility.Visible;
			gridSendMessage.Visibility = Visibility.Collapsed;
		}

		private async Task<string> SendMessage ( string toUserId )
		{
			dynamic dataToSend = new
			{
				toUserId = toUserId ,
				fromUserId = UserData.id ,
				message = textBoxContent.Text
			};
			string result = await RestAPI.SendJson ( dataToSend , RestAPI.phpAddress , "SendMessage" );
			dynamic json = JObject.Parse ( result );
			string status = json.status;
			if ( status == "200" )
			{
				return "OK";
			}
			else
			{
				return "";
			}
		}

		private async void SendMessageTap ( object sender , TappedRoutedEventArgs e )
		{
			//check length
			string toUserId = ( ( Button ) sender ).Tag.ToString ();
			string r = await SendMessage ( toUserId );
			if ( r == "OK" )
			{
				gridSendMessage.Visibility = Visibility.Collapsed;
				stackPanel.Visibility = Visibility.Visible;
			}
			else
			{
				CustomNotification.ShowDialogMessage ();
			}
		}
	}
}
