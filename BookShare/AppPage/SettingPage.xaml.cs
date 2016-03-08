using BookShare.Helper;
using BookShare.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Windows.Storage;
using Windows.UI.Notifications;
using Windows.UI.Popups;
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
			GetUserInfo ();
			progressBar.Visibility = Visibility.Visible;
			relativePanel.Visibility = Visibility.Collapsed;
		}

		protected override void OnNavigatedTo ( NavigationEventArgs e )
		{
			mes = new MessageDialog ( "" );
		}

		private ObservableCollection<District> district;
		private ObservableCollection<City> city;
		private User user;
		MessageDialog mes;

		private void DisplayUserInfo ()
		{
			this.DataContext = user;
			userAva.Source = new Windows.UI.Xaml.Media.Imaging.BitmapImage ( new Uri ( user.ava ) );
			//check if user city is null
			if ( user.cityId != null )
			{
				//find user city object
				object c = city.First ( o => o.cityId == user.cityId );
				comboCity.SelectedItem = c;
			}

			//check if user district is null
			if ( user.districtId != null )
			{
				//find user district object
				object d = district.First ( o => o.districtId == user.districtId );
				comboDistrict.SelectedItem = d;
			}
		}

		private async void GetUserInfo ()
		{
			string sendResult = await RestAPI.SendJson ( UserData.id , RestAPI.phpAddress , "GetAccountInfo" );
			try
			{
				string data = JsonHelper.DecodeJson ( sendResult );
				Dictionary<string , object> d = JsonConvert.DeserializeObject<Dictionary<string , object>> ( data );
				user = JsonHelper.ConvertToUser ( d["user"].ToString () );
				user.id = UserData.id;
				user.SetAva ();

				//deserialize all location into list
				district = JsonHelper.ConvertToDistricts ( d["allDistrict"].ToString () );
				city = JsonHelper.ConvertToCities ( d["allCity"].ToString () );

				comboCity.ItemsSource = city;
				comboDistrict.ItemsSource = district;
				DisplayUserInfo ();

				progressBar.Visibility = Visibility.Collapsed;
				relativePanel.Visibility = Visibility.Visible;
			}
			catch ( Exception ex )
			{
				CustomNotification.ShowDialogMessage ( content: ex.Message );
			}
		}

		private async void SaveClick ( object sender , RoutedEventArgs e )
		{
			if ( !RegexUtilities.IsValidEmail ( textBoxEmail.Text ) )
			{
				mes.Content = "Email không hợp lệ, kiểm tra lại";
				await mes.ShowAsync ();
			}
			else
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
				string result = await RestAPI.SendJson ( newUser , RestAPI.phpAddress , "SetAccountInfo" );
				//show new value
				DisplayUserInfo ();
				CustomNotification.ShowNotification ( "Cập nhật thành công" );
				ToastNotificationManager.History.Clear ();
			}
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
				comboDistrict.ItemsSource =
				district.Where ( o => o.cityId == comboCity.SelectedValue.ToString () );
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
	}
}
