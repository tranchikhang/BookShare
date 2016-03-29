using BookShare.Helper;
using BookShare.Model;
using System;
using System.Threading.Tasks;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
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
			NavigationMethod.SetBackButtonVisibility ( true );
			SystemNavigationManager.GetForCurrentView ().BackRequested += BackButtonClick;
		}

		private void BackButtonClick ( object sender , BackRequestedEventArgs e )
		{
			if ( Frame.CanGoBack )
				Frame.GoBack ();
		}

		protected override void OnNavigatedFrom ( NavigationEventArgs e )
		{
			SystemNavigationManager.GetForCurrentView ().BackRequested -= BackButtonClick;
			NavigationMethod.SetBackButtonVisibility ( false );
		}

		private async void RegisterClick ( object sender , RoutedEventArgs e )
		{
			string r = FieldValidation ();
			if ( r == "" )
			{
				ControlMethods.SwitchVisibility ( true , progressBar );
				await SendRegistrationData ( textBoxUser.Text , textBoxEmail.Text , textBoxFullName.Text , pwBox.Password );
				ControlMethods.SwitchVisibility ( false , progressBar );
			}
			else
			{
				gridNotification.Show ( true , r );
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

		private async Task SendRegistrationData ( string user , string email , string fullname , string password )
		{
			dynamic acc = new
			{
				user = user ,
				email = email ,
				fullname = fullname ,
				password = password
			};
			//string result = await RestAPI.SendJson ( acc , RestAPI.phpAddress , "Register" );
			string result = await RestAPI.SendPostRequest ( acc , RestAPI.publicApiAddress + "register/" );
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
				gridNotification.Show ( true , JsonHelper.GetJsonMessage ( result ) );
			}
		}
	}
}
