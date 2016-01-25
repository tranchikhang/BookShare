using BookShare.Helper;
using BookShare.Model;
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
			MessageDialog mes = new MessageDialog ("");
			mes.Title = "Thông báo";
			//check username
			if (textBoxUser.Text =="")
			{
				mes.Content = "Bạn chưa nhập tên người dùng";
				await mes.ShowAsync ();
			}
			else
			{
				//check password
				if (pwBox.Password=="")
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
			LoginAccount login = new LoginAccount ( user , password );
			string result = await RestAPI.SendJson ( login , RestAPI.phpAdress + "client/account/login.php" , "login" );
			if (result == "PASS")
			{
				//navigate mainpage
			}
			else
			{
				MessageDialog dialog = new MessageDialog ( result );
				await dialog.ShowAsync ();
			}
		}
	}
}
