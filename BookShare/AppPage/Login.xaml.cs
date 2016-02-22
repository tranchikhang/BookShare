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
	public sealed partial class Login : Page
	{
		public Login ()
		{
			this.InitializeComponent ();
		}

		private async void LoginClick ( object sender , RoutedEventArgs e )
		{
			MessageDialog mes = new MessageDialog ( "" );
			mes.Title = "Thông báo";
			//check username
			if ( textBoxUser.Text == "" )
			{
				mes.Content = "Bạn chưa nhập tên người dùng";
				await mes.ShowAsync ();
			}
			else
			{
				//check password
				if ( pwBox.Password == "" )
				{
					mes.Content = "Bạn chưa nhập mật khẩu";
					await mes.ShowAsync ();
				}
				else
				{
					SendLoginData ( textBoxUser.Text , pwBox.Password );
				}
			}
		}

		private async void SendLoginData ( string user , string password )
		{
			dynamic login = new
			{
				user = user ,
				password = password
			};
			string result = await RestAPI.SendJson ( login , RestAPI.phpAddress , "Login" );
			dynamic json = JObject.Parse ( result );
			string status = json.status;
			string message = json.message;
			if ( status == "200" )
			{
				//save user token
				UserData.token = message;
				UserData.settings.Add ( AppSettings.keyToken , UserData.token );
				//save user id
				UserData.id = json.id;
				if ( UserData.settings.Contain ( AppSettings.keyId ) )
					UserData.settings.Update ( AppSettings.keyId, UserData.id );
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
				MessageDialog dialog = new MessageDialog ( message );
				await dialog.ShowAsync ();
			}
		}
	}
}
