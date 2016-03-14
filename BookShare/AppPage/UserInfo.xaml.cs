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
			ControlMethods.SwitchVisibility ( true , progressBar );
			stackPanel.Visibility = Visibility.Collapsed;
			gridSendMessage.Visibility = Visibility.Collapsed;

			timer = new DispatcherTimer ();
			timer.Interval = TimeSpan.FromSeconds ( 1 );
			timer.Tick += new EventHandler<object> ( TimerTick );
		}

		private void TimerTick ( object sender , object e )
		{
			timer.Stop ();
			ControlMethods.SwitchVisibility ( false , progressBar );
			if ( gridSendMessage.Visibility == Visibility.Visible )
				gridSendMessage.Visibility = Visibility.Collapsed;
			if ( stackPanel.Visibility == Visibility.Collapsed )
				stackPanel.Visibility = Visibility.Visible;
		}

		private User selectedUser;
		DispatcherTimer timer;

		protected override void OnNavigatedTo ( NavigationEventArgs e )
		{
			string userId = e.Parameter as String;
			LoadData ( userId );
			timer.Start ();
		}

		private async void LoadData ( string userId )
		{
			//get user info
			string result = await RestAPI.SendJson ( userId , RestAPI.phpAddress , "GetUserInfo" );
			if ( JsonHelper.IsRequestSucceed ( result ) == RestAPI.ResponseStatus.OK )
			{
				string data = JsonHelper.DecodeJson ( result );
				selectedUser = JsonHelper.ConvertToUser ( data );
				selectedUser.SetAddress ();
				selectedUser.SetAva ();
				this.DataContext = selectedUser;
				//if timer is still running, then do nothing
				if ( !timer.IsEnabled )
					ControlMethods.SwitchVisibility ( false , progressBar );
			}
		}

		private void SendTap ( object sender , TappedRoutedEventArgs e )
		{
			stackPanel.Visibility = Visibility.Collapsed;
			gridSendMessage.Visibility = Visibility.Visible;
		}

		private void BackClick ( object sender , RoutedEventArgs e )
		{
			stackPanel.Visibility = Visibility.Visible;
			gridSendMessage.Visibility = Visibility.Collapsed;
		}

		private async void SendMessageTap ( object sender , TappedRoutedEventArgs e )
		{
			//check length
			if ( textBoxContent.Text.Length >= 300 )
				CustomNotification.ShowDialogMessage ( content: "Tin nhắn không được quá 300 ký tự" );
			else if ( textBoxContent.Text.Length > 0 )
			{
				//start the timer
				timer.Start ();
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
				string result = await RestAPI.SendJson ( dataToSend , RestAPI.phpAddress , "SendMessage" );
				if ( JsonHelper.IsRequestSucceed ( result ) == RestAPI.ResponseStatus.OK )
				{
					//clear message
					textBoxContent.Text = "";
					//if timer is still running, then do nothing
					if ( !timer.IsEnabled )
						ControlMethods.SwitchVisibility ( false , progressBar );
				}
				else
				{
					CustomNotification.ShowDialogMessage ();
				}
			}
		}
	}
}
