using BookShare.Helper;
using BookShare.Model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.ObjectModel;
using System.Linq;
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
			textBoxAccount.Text = user.account;
			textBoxEmail.Text = user.email;
			textBoxFullName.Text = ( user.fullname == null ) ? "" : user.fullname;
			textBoxAddress.Text = ( user.address == null ) ? "" : user.address;

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
			
			progressBar.Visibility = Visibility.Collapsed;
			relativePanel.Visibility = Visibility.Visible;
		}

		private async void GetUserInfo ()
		{
			string sendResult = await RestAPI.SendJson ( UserData.id , RestAPI.phpAddress , "GetAccountInfo" );
			dynamic json = JObject.Parse ( sendResult );
			string status = json.status;
			if ( status == "200" )
			{
				//deserialize user
				user = new User
				{
					account = json.user.account ,
					email = json.user.email ,
					fullname = json.user.fullname ,
					address = json.user.address ,
					districtId = json.user.districtId ,
					cityId = json.user.cityId ,
				};
				//deserialize all location into list
				district = new ObservableCollection<District> ();


				for ( int i = 0 ; i < json.allDistrict.Count ; i++ )
				{
					district.Add ( new District
					{
						districtId = json.allDistrict[i].districtId ,
						districtName = json.allDistrict[i].districtName ,
						cityId = json.allDistrict[i].cityId
					} );
				}

				city = new ObservableCollection<City> ();
				for ( int i = 0 ; i < json.allCity.Count ; i++ )
				{
					city.Add ( new City
					{
						cityId = json.allCity[i].cityId ,
						cityName = json.allCity[i].cityName
					} );
				}

				comboCity.ItemsSource = city;
				comboDistrict.ItemsSource = district;
				DisplayUserInfo ();
			}
			else
			{
				CustomNotification.ShowNotification ( "loi" );
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
				dynamic newUser = new
				{
					userId = UserData.id ,
					email = textBoxEmail.Text ,
					fullname = textBoxFullName.Text ,
					address = textBoxAddress.Text ,
					districtId = comboDistrict.SelectedValue ,
				};
				string result = await RestAPI.SendJson ( newUser , RestAPI.phpAddress , "SetAccountInfo" );
				//show new value
				DisplayUserInfo ();
				CustomNotification.ShowNotification("Cập nhật thành công");
				ToastNotificationManager.History.Clear ();
			}
		}

		private void UpdateLocalInfo ()
		{
			user.email = textBoxEmail.Text;
			user.fullname = textBoxFullName.Text;
			user.address = textBoxAddress.Text;
			user.districtId = comboDistrict.SelectedValue.ToString();
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
	}
}
