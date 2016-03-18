using BookShare.Helper;
using BookShare.Model;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using System;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace BookShare.AppPage
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class MessagePage : Page
	{
		public MessagePage ()
		{
			this.InitializeComponent ();
			ControlMethods.SwitchVisibility ( true , progressBar );
			ControlMethods.SwitchVisibility ( false , listViewConversation );
			GetMessages ();
		}

		private ObservableCollection<Conversation> conversations;
		private Conversation currentConversations;

		private async void GetMessages ()
		{
			string result = await RestAPI.SendJson ( UserData.id , RestAPI.phpAddress , "GetMessages" );
			if ( JsonHelper.IsRequestSucceed ( result ) == RestAPI.ResponseStatus.OK )
			{
				string data = JsonHelper.DecodeJson ( result );
				if ( data != "" )
				{
					conversations = JsonHelper.ConverToConversations ( data );
					foreach ( Conversation c in conversations )
					{
						c.CheckNewMessage ();
						c.SetLastMessage ();
						c.SetAva ();
					}
					listViewConversation.ItemsSource = conversations;
					ControlMethods.SwitchVisibility ( true , listViewConversation );
				}
			}
			else
			{
				ShowNotification ( "Bạn không có tin nhắn nào" );
			}
			if ( result != null )
				ControlMethods.SwitchVisibility ( false , progressBar );
		}

		private async void CovnersationGridTapped ( object sender , TappedRoutedEventArgs e )
		{
			//get userId from user
			string fromUserId = ( ( Grid ) sender ).Tag.ToString ();

			//set current conversation
			currentConversations = null;
			currentConversations = conversations.First ( c => c.userId == fromUserId );

			//if conversation has new message, then mark as red
			if ( currentConversations.isNewMessage )
			{
				RestAPI.ResponseStatus r = await MarkReadConversation ( fromUserId );
				if ( r == RestAPI.ResponseStatus.Failed )
				{
					CustomNotification.ShowDialogMessage ();
				}
				else
				{
					//mark as read
					currentConversations.isNewMessage = false;
				}
			}
			//show back button
			SystemNavigationManager.GetForCurrentView ().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
			//back button event
			SystemNavigationManager.GetForCurrentView ().BackRequested += BackButtonClick;

			listViewConversation.Visibility = Visibility.Collapsed;

			//get all message in conversation
			//check if message is from user
			foreach ( Message m in currentConversations.messages )
			{
				m.isSender = ( m.fromUserId == fromUserId ) ? true : false;
			}
			listViewMessage.ItemsSource = currentConversations.messages;
			//scroll to end
			listViewMessage.SelectedIndex = listViewMessage.Items.Count - 1;
			listViewMessage.UpdateLayout ();
			listViewMessage.ScrollIntoView ( listViewMessage.SelectedItem );
			gridMessages.Visibility = Visibility.Visible;
			gridSendBox.DataContext = currentConversations;
		}

		private async Task<RestAPI.ResponseStatus> MarkReadConversation ( string fromUserId )
		{
			dynamic dataToSend = new
			{
				toUserId = UserData.id ,
				fromUserId = fromUserId ,
			};
			string result = await RestAPI.SendJson ( dataToSend , RestAPI.phpAddress , "MarkReadConversation" );
			return JsonHelper.IsRequestSucceed ( result );
		}

		private void BackButtonClick ( object sender , BackRequestedEventArgs e )
		{
			gridMessages.Visibility = Visibility.Collapsed;
			ControlMethods.SwitchVisibility ( true , listViewConversation );

			SystemNavigationManager.GetForCurrentView ().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Collapsed;
			//refresh conversation binding
			listViewConversation.ItemsSource = null;
			listViewConversation.ItemsSource = conversations;
		}

		private async void SendMessageTap ( object sender , TappedRoutedEventArgs e )
		{
			//check length
			if ( CheckMessageLength () == "" )
			{
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
					string dateTime = JsonHelper.DecodeJson ( result );
					Message newMes = new Message ( UserData.id , toUserId , textBoxContent.Text , dateTime );
					currentConversations.messages.Add ( newMes );
					textBoxContent.Text = "";
				}
				else
				{
					ShowNotification ( "Có lỗi, thử lại sau" );
				}
			}
			else
				ShowNotification ( CheckMessageLength () );
		}

		private string CheckMessageLength ()
		{
			if ( textBoxContent.Text.Length >= 300 )
				return "Tin nhắn không được quá 300 ký tự";
			if ( textBoxContent.Text.Length == 0 )
				return "Nhập nội dung tin nhắn";
			return "";
		}

		private void ShowNotification ( string content )
		{
			//notify user
			textBlockContent.Text = content;
			gridNotification.Visibility = Visibility.Visible;
		}

		private void NotificationDismiss ( object sender , RoutedEventArgs e )
		{
			textBlockContent.Text = "";
			gridNotification.Visibility = Visibility.Collapsed;
		}
	}
}
