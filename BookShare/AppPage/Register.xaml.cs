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
			SetWindowSize ();
		}

		private void SetWindowSize ()
		{
			//default size
			ApplicationView.PreferredLaunchViewSize = new Size ( 480 , 640 );
			ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;
			//minimum resize
			ApplicationView.GetForCurrentView ().SetPreferredMinSize ( new Size ( 480 , 500 ) );
		}

		private async void RegisterClick ( object sender , RoutedEventArgs e )
		{
			MessageDialog mes = new MessageDialog ( "" );
			mes.Title = "Thông báo";
			//check policy
			if ( !IsPolicyChecked () )
			{
				mes.Content = "Bạn chưa đồng ý các điều khoản";
				await mes.ShowAsync ();
			}
			else
			{
				//check account
				if ( textBoxUser.Text == "" )
				{
					mes.Content = "Tên tài khoản không hợp lệ, kiểm tra lại";
					await mes.ShowAsync ();
				}
				else
				{
					//check email
					if ( !RegexUtilities.IsValidEmail ( textBoxEmail.Text ) )
					{
						mes.Content = "Email không hợp lệ, kiểm tra lại";
						await mes.ShowAsync ();
					}
					else
					{
						//check 2 password
						if ( pwBox.Password != pwBoxRe.Password && pwBox.Password != "" )
						{
							mes.Content = "Mật khẩu không chính xác, kiểm tra lại";
							await mes.ShowAsync ();
						}
						else
						{
							SendRegistrationData ( textBoxUser.Text , textBoxEmail.Text , textBoxFullName.Text , pwBox.Password );
						}
					}
				}
			}
		}

		private async void SendRegistrationData ( string user , string email , string fullname , string password )
		{
			RegisterAccount acc = new RegisterAccount ( user , email , fullname , password );
			string result = await RestAPI.SendJson ( acc , RestAPI.phpAdress + "client/account/register.php" , "register" );
			MessageDialog dialog = new MessageDialog ( result );
			await dialog.ShowAsync ();
		}

		private bool IsPolicyChecked ()
		{
			if ( chkBoxPolicy.IsChecked == true )
				return true;
			else return false;
		}
	}
}
