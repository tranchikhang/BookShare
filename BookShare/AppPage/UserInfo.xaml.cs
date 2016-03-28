using BookShare.Helper;
using BookShare.Model;
using System;
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
	public sealed partial class UserInfo : Page
	{
		public UserInfo ()
		{
			this.InitializeComponent ();

			NavigationMethod.SetBackButtonVisibility ( true );
			SystemNavigationManager.GetForCurrentView ().BackRequested += BackButtonClick;

		}

		private User selectedUser;

		protected override void OnNavigatedTo ( NavigationEventArgs e )
		{
			string userId = e.Parameter as String;
			LoadData ( userId );

		}

		protected override void OnNavigatedFrom ( NavigationEventArgs e )
		{
			SystemNavigationManager.GetForCurrentView ().BackRequested -= BackButtonClick;
		}

		private async void LoadData ( string userId )
		{
			//get user info
			//string result = await RestAPI.SendJson ( userId , RestAPI.phpAddress , "GetUserInfo" );
			string result = await RestAPI.SendGetRequest ( RestAPI.publicApiAddress + "users/" + userId );
			if ( JsonHelper.IsRequestSucceed ( result ) == RestAPI.ResponseStatus.OK )
			{
				string data = JsonHelper.DecodeJson ( result );
				selectedUser = JsonHelper.ConvertToUser ( data );
				selectedUser.SetAddress ();
				selectedUser.SetAva ();
				DataContext = selectedUser;
				scrollViewer.Visibility = Visibility.Visible;
			}
			else gridNotification.Show ( true );
			ControlMethods.SwitchVisibility ( false , progressBar );
		}

		private void SendTap ( object sender , TappedRoutedEventArgs e )
		{
			scrollViewer.Visibility = Visibility.Collapsed;
			gridSendMessage.Visibility = Visibility.Visible;
		}

		private async void SendMessageTap ( object sender , TappedRoutedEventArgs e )
		{
			string r = MessageValidation ();
			if ( r == "" && textBoxContent.Text.Length > 0 )
			{
				//show progressbar
				ControlMethods.SwitchVisibility ( true , progressBar );
				//send the message
				string toUserId = ( ( Button ) sender ).Tag.ToString ();
				dynamic dataToSend = new
				{
					toUserId = toUserId ,
					fromUserId = UserData.id ,
					message = textBoxContent.Text
				};
				//string result = await RestAPI.SendJson ( dataToSend , RestAPI.phpAddress , "SendMessage" );
				string result =
					await RestAPI.SendPostRequest ( dataToSend , RestAPI.publicApiAddress + "message/send/" );
				if ( JsonHelper.IsRequestSucceed ( result ) == RestAPI.ResponseStatus.OK )
				{
					//clear message
					textBoxContent.Text = "";
				}
				else
				{
					gridNotification.Show ( true );
				}
				ControlMethods.SwitchVisibility ( false , progressBar );
				gridSendMessage.Visibility = Visibility.Collapsed;
				scrollViewer.Visibility = Visibility.Visible;
			}
			else
			{
				gridNotification.Show ( true , r );
			}
		}

		private string MessageValidation ()
		{
			if ( textBoxContent.Text.Length >= 300 )
				return "Tin nhắn không được quá 300 ký tự";
			return "";
		}

		private void BackButtonClick ( object sender , BackRequestedEventArgs e )
		{
			if ( gridSendMessage.Visibility == Visibility.Visible )
			{
				gridSendMessage.Visibility = Visibility.Collapsed;
				scrollViewer.Visibility = Visibility.Visible;
			}
			else
			{
				Frame rootFrame = NavigationMethod.GetMainFrame ();

				// Navigate back if possible, and if the event has not 
				// already been handled .
				if ( rootFrame.CanGoBack )
				{
					rootFrame.GoBack ();
				}
			}
		}
	}
}
