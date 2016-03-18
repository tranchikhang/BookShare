using BookShare.Helper;
using BookShare.Model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.ViewManagement;
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
	public sealed partial class Register : Page
	{
		public Register ()
		{
			this.InitializeComponent ();
			gridNotification.Visibility = Visibility.Collapsed;
		}

		private void RegisterClick ( object sender , RoutedEventArgs e )
		{
			string r = FieldValidation ();
			if ( r == "" )
			{
				SendRegistrationData ( textBoxUser.Text , textBoxEmail.Text , textBoxFullName.Text , pwBox.Password );
			}
			else
			{
				ShowNotification ( r );
			}
		}

		private string FieldValidation ()
		{
			if ( chkBoxPolicy.IsChecked != true )
				return "Bạn chưa đồng ý các điều khoản";
			if ( textBoxUser.Text == "" )
				return "Tên tài khoản không hợp lệ, kiểm tra lại";
			if ( textBoxUser.Text.Length > 30 )
				return "Tên tài khoản phải dưới 30 ký tự";
			if ( textBoxFullName.Text.Length > 32 )
				return "Tên đầy đủ phải dưới 32 ký tự";
			if ( !RegexUtilities.IsValidEmail ( textBoxEmail.Text ) )
				return "Email không hợp lệ, kiểm tra lại";
			if ( pwBox.Password.Length > 16 || pwBox.Password.Length < 5 )
				return "Độ dài mật khẩu phải từ 5-16 ký tự";
			if ( pwBox.Password != pwBoxRe.Password && pwBox.Password != "" )
				return "Mật khẩu không chính xác, kiểm tra lại";
			return "";
		}

		private User newUser;

		private async void SendRegistrationData ( string user , string email , string fullname , string password )
		{
			dynamic acc = new
			{
				user = user ,
				email = email ,
				fullname = fullname ,
				password = password
			};
			string result = await RestAPI.SendJson ( acc , RestAPI.phpAddress , "Register" );
			if ( JsonHelper.IsRequestSucceed ( result ) == RestAPI.ResponseStatus.OK )
			{
				string data = JsonHelper.DecodeJson ( result );
				newUser = JsonHelper.ConvertToUser ( data );
				//save user token
				UserData.token = newUser.token;
				UserData.settings.Add ( AppSettings.keyToken , UserData.token );
				//save user id
				UserData.id = newUser.id;
				if ( UserData.settings.Contain ( AppSettings.keyId ) )
					UserData.settings.Update ( AppSettings.keyId , UserData.id );
				else
					UserData.settings.Add ( AppSettings.keyId , UserData.id );
				//save opened status
				if ( UserData.settings.Contain ( AppSettings.keyFirstOpen ) )
					UserData.settings.Update ( AppSettings.keyFirstOpen , false );
				else
					UserData.settings.Add ( AppSettings.keyFirstOpen , false );
				//navigate to mainpage
				Frame.Navigate ( typeof ( MainPage ) );
			}
			else
			{
				ShowNotification ( JsonHelper.GetJsonMessage ( result ) );
			}
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
