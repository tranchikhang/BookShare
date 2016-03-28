using BookShare.Helper;
using BookShare.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Windows.Security.Cryptography;
using Windows.Security.Cryptography.Core;
using Windows.Storage;
using Windows.Storage.Streams;
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
	public sealed partial class SettingPage : Page
	{
		public SettingPage ()
		{
			this.InitializeComponent ();
			progressBar.Visibility = Visibility.Visible;
			mainScrollViewer.Visibility = Visibility.Collapsed;
			NavigationMethod.SetBackButtonVisibility ( false );
			GetData ();
			progressBar.Visibility = Visibility.Collapsed;
			mainScrollViewer.Visibility = Visibility.Visible;
		}

		private async void GetData ()
		{
			try
			{
				await GetLocation ();
				await GetUserInfo ();
			}
			catch ( Exception e )
			{
				gridNotification.Show ( true );
			}
		}

		protected override void OnNavigatedTo ( NavigationEventArgs e )
		{
		}

		protected override void OnNavigatedFrom ( NavigationEventArgs e )
		{
			SystemNavigationManager.GetForCurrentView ().BackRequested -= BackButtonClick;
		}

		private ObservableCollection<City> city;
		private User user;

		private void DisplayUserInfo ()
		{
			this.DataContext = user;
			userAva.Source = new Windows.UI.Xaml.Media.Imaging.BitmapImage ( new Uri ( user.ava ) );
			//check if user city is null
			if ( user.cityId != null )
			{
				//find user city object
				object c = city.First ( o => o.id == user.cityId );
				comboCity.SelectedItem = c;
			}

			//check if user district is null
			if ( user.districtId != null )
			{
				comboDistrict.ItemsSource = ( ( City ) comboCity.SelectedItem ).districts;
				comboDistrict.SelectedItem =
					( ( City ) comboCity.SelectedItem ).districts.First ( o => o.id == user.districtId );
			}
		}

		public async Task GetLocation ()
		{
			string result = await RestAPI.SendGetRequest ( RestAPI.publicApiAddress + "location/" );
			if ( JsonHelper.IsRequestSucceed ( result ) == RestAPI.ResponseStatus.OK )
			{
				string data = JsonHelper.DecodeJson ( result );
				//deserialize all location into list
				city = JsonHelper.ConvertToCities ( data );

				comboCity.ItemsSource = city;
			}
		}
		private async Task GetUserInfo ()
		{
			//string result = await RestAPI.SendJson ( UserData.id , RestAPI.phpAddress , "GetAccountInfo" );
			string result = await RestAPI.SendGetRequest ( RestAPI.publicApiAddress + "account/" + UserData.id );
			if ( JsonHelper.IsRequestSucceed ( result ) == RestAPI.ResponseStatus.OK )
			{
				string data = JsonHelper.DecodeJson ( result );
				user = JsonHelper.ConvertToUser ( data );
				user.id = UserData.id;
				user.SetAva ();
				DisplayUserInfo ();
			}
		}

		private async void SaveClick ( object sender , RoutedEventArgs e )
		{
			string r = Fieldvalidation ();
			if ( r == "" )
			{
				UpdateLocalInfo ();
				string imageString = "";
				if ( file != null )
					imageString = await ImageUpload.StorageFileToBase64 ( file );
				else
					imageString = "";
				dynamic newUser = new
				{
					userId = UserData.id ,
					email = textBoxEmail.Text ,
					fullname = textBoxFullName.Text ,
					address = textBoxAddress.Text ,
					districtId = comboDistrict.SelectedValue ,
					ava = imageString
				};
				//string result = await RestAPI.SendJson ( newUser , RestAPI.phpAddress , "SetAccountInfo" );
				string result = await RestAPI.SendPostRequest ( newUser , RestAPI.publicApiAddress + "account/update/" );
				if ( JsonHelper.IsRequestSucceed ( result ) == RestAPI.ResponseStatus.OK )
				{
					gridNotification.Show ( false , "Cập nhật thành công" );
					//show new value
					DisplayUserInfo ();
				}
				else gridNotification.Show ( true );
			}
			else
			{
				gridNotification.Show ( true , r );
			}
		}

		private string Fieldvalidation ()
		{
			if ( textBoxEmail.Text == "" || !RegexUtilities.IsValidEmail ( textBoxEmail.Text ) )
				return "Email không hợp lệ, kiểm tra lại";
			if ( textBoxFullName.Text.Length > 30 )
				return "Tên không hợp lệ, kiểm tra lại";
			if ( textBoxAddress.Text.Length > 30 )
				return "Địa chỉ không hợp lệ, kiểm tra lại";
			return "";
		}

		private void UpdateLocalInfo ()
		{
			user.email = textBoxEmail.Text;
			user.fullname = textBoxFullName.Text;
			user.address = textBoxAddress.Text;
			user.districtId = comboDistrict.SelectedValue.ToString ();
			user.cityId = comboCity.SelectedValue.ToString ();
		}

		private void comboCity_SelectionChanged ( object sender , SelectionChangedEventArgs e )
		{
			if ( comboCity.SelectedValue != null )
			{
				comboDistrict.ItemsSource = ( ( City ) comboCity.SelectedItem ).districts;
			}
		}

		private StorageFile file;

		private async void AddFileClick ( object sender , RoutedEventArgs e )
		{

			var picker = new Windows.Storage.Pickers.FileOpenPicker ();
			picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail;
			picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.PicturesLibrary;
			picker.FileTypeFilter.Add ( ".jpg" );

			file = await picker.PickSingleFileAsync ();
		}

		private void ChangePassClick ( object sender , RoutedEventArgs e )
		{
			mainScrollViewer.Visibility = Visibility.Collapsed;
			gridChangePass.Visibility = Visibility.Visible;
			pwBoxCurrent.Password = "";
			pwBoxNew.Password = "";
			pwBoxNewRe.Password = "";
			//show back button
			NavigationMethod.SetBackButtonVisibility ( true );
			//back button event
			SystemNavigationManager.GetForCurrentView ().BackRequested += BackButtonClick;
		}

		private void BackButtonClick ( object sender , BackRequestedEventArgs e )
		{
			mainScrollViewer.Visibility = Visibility.Visible;
			gridChangePass.Visibility = Visibility.Collapsed;
			NavigationMethod.SetBackButtonVisibility ( false );
		}

		private async void ChangePass ( object sender , RoutedEventArgs e )
		{
			string r = CheckPasswordValid ();
			if ( r == "" )
			{
				//ok
				dynamic dataToSend = new
				{
					userNewPass = pwBoxNew.Password ,
					userId = UserData.id
				};
				//string result = await RestAPI.SendJson ( dataToSend , RestAPI.phpAddress , "ChangePass" );
				string result = await RestAPI.SendPostRequest ( dataToSend , RestAPI.publicApiAddress + "account/password/" );
				if ( JsonHelper.IsRequestSucceed ( result ) == RestAPI.ResponseStatus.OK )
				{
					user.password = ComputeMD5 ( pwBoxNew.Password );
					gridChangePass.Visibility = Visibility.Collapsed;
					mainScrollViewer.Visibility = Visibility.Visible;
					NavigationMethod.SetBackButtonVisibility ( false );
				}
				else
					gridNotification.Show ( true );
			}
			else
			{
				gridNotification.Show ( true , r );
			}

		}
		private string CheckPasswordValid ()
		{
			//check empty field
			if ( pwBoxCurrent.Password == "" || pwBoxNew.Password == "" || pwBoxNewRe.Password == "" )
				return "Điền thông tin vào các trường";
			//check if current password matched old password
			if ( ComputeMD5 ( pwBoxCurrent.Password ) != user.password )
				return "Mật khẩu hiện tại không đúng";
			//new password must be different from old one
			if ( pwBoxCurrent.Password == pwBoxNew.Password )
				return "Mật khẩu mới không được giống mật khẩu cũ";
			//check new password length
			if ( pwBoxNew.Password.Length > 16 || pwBoxNew.Password.Length < 5 )
				return "Độ dài mật khẩu phải từ 5-16 ký tự";
			//check new password recheck
			if ( pwBoxNew.Password != pwBoxNewRe.Password )
				return "Xác nhận lại mật khẩu mới";
			return "";
		}

		public string ComputeMD5 ( string str )
		{
			var alg = HashAlgorithmProvider.OpenAlgorithm ( HashAlgorithmNames.Md5 );
			IBuffer buff = CryptographicBuffer.ConvertStringToBinary ( str , BinaryStringEncoding.Utf8 );
			var hashed = alg.HashData ( buff );
			var res = CryptographicBuffer.EncodeToHexString ( hashed );
			return res;
		}

		private void LogoutClick ( object sender , RoutedEventArgs e )
		{
			//remove user token
			UserData.settings.Remove ( AppSettings.keyToken );
			//remove user id
			UserData.settings.Remove ( AppSettings.keyId );
			//remove opened status
			UserData.settings.Remove ( AppSettings.keyFirstOpen );
			NavigationMethod.GetTopFrame ().Navigate ( typeof ( StartPage ) );
		}
	}
}
