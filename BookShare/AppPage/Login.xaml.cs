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
	public sealed partial class Login : Page
	{
		public Login ()
		{
			this.InitializeComponent ();
			NavigationMethod.SetBackButtonVisibility ( true );
			SystemNavigationManager.GetForCurrentView ().BackRequested += BackButtonClick;
		}

		protected override void OnNavigatedFrom ( NavigationEventArgs e )
		{
			SystemNavigationManager.GetForCurrentView ().BackRequested -= BackButtonClick;
			NavigationMethod.SetBackButtonVisibility ( false );
		}

		private void BackButtonClick ( object sender , BackRequestedEventArgs e )
		{
			if ( Frame.CanGoBack )
				Frame.GoBack ();
		}

		private async void LoginClick ( object sender , RoutedEventArgs e )
		{
			ControlMethods.SwitchVisibility ( true , progressBar );
			string r = FieldValidation ();
			ControlMethods.SwitchVisibility ( false , progressBar );
			if ( r == "" )
			{
				ControlMethods.SwitchVisibility ( true , progressBar );
				await SendLoginData ( textBoxUser.Text , pwBox.Password );
				ControlMethods.SwitchVisibility ( false , progressBar );
			}
			else
			{
				gridNotification.Show ( true , r );
			}
		}

		private string FieldValidation ()
		{
			if ( textBoxUser.Text == "" )
				return "Bạn chưa nhập tên người dùng";
			if ( textBoxUser.Text.Length<5 )
				return "Tên người dùng tối thiểu 5 kí tự";
			if ( pwBox.Password == "" )
				return "Bạn chưa nhập mật khẩu";
			if ( pwBox.Password.Length < 5 )
				return "Mật khẩu tối thiểu 5 kí tự";
			return "";
		}

		private User newUser;

		private async Task SendLoginData ( string user , string password )
		{
			dynamic login = new
			{
				user = user ,
				password = password
			};
			string result = await RestAPI.SendPostRequest ( login , RestAPI.publicApiAddress + "login/" );
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
