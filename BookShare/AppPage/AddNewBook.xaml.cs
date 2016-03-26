using BookShare.Helper;
using BookShare.Model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace BookShare.AppPage
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class AddNewBook : Page
	{
		public AddNewBook ()
		{
			InitializeComponent ();
			AddGenre ();
			AddYear ();
		}

		private StorageFile file;

		private void AddYear ()
		{
			for ( int i = 1950 ; i < 2020 ; i++ )
			{
				comboYear.Items.Add ( i );
			}
		}

		private async void AddGenre ()
		{
			string result = await RestAPI.SendJson ( "" , RestAPI.phpAddress , "GetAllGenre" );
			dynamic json = JArray.Parse ( result );
			var l = new List<object> ();
			for ( int i = 0 ; i < json.Count ; i++ )
			{
				l.Add ( new
				{
					gid = json[i].gid ,
					gname = json[i].gname
				} );
			}
			comboBoxGenre.ItemsSource = l;
		}


		private async void AddFileClick ( object sender , RoutedEventArgs e )
		{
			var picker = new Windows.Storage.Pickers.FileOpenPicker ();
			picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail;
			picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.PicturesLibrary;
			picker.FileTypeFilter.Add ( ".jpg" );

			file = await picker.PickSingleFileAsync ();

			if ( file != null )
			{
				textBlockFile.Text = "Ảnh đã chọn: " + file.Name;
			}
			else
			{
				textBlockFile.Text = "";
			}

		}

		private async void AddBook ( object sender , RoutedEventArgs e )
		{
			string v = FieldValidation ();
			if ( v == "" )
			{
				string imageString = await ImageUpload.StorageFileToBase64 ( file );
				dynamic dataTosend = new
				{
					title = textBoxTitle.Text ,
					author = suggestAuthor.Text ,
					year = comboYear.SelectedValue ,
					genreId = comboBoxGenre.SelectedValue ,
					description = textBoxDes.Text ,
					image = imageString ,
					userId = UserData.id
				};
				string result = await RestAPI.SendJson ( dataTosend , RestAPI.phpAddress , "AddNewBook" );
				if ( JsonHelper.IsRequestSucceed ( result ) == RestAPI.ResponseStatus.OK )
				{
					gridNotification.Show ( true , "Đã thêm sách mới" );
				}
			}
			else
			{
				gridNotification.Show ( false , v );
			}
		}

		private string FieldValidation ()
		{
			//check empty
			if ( textBlockFile.Text == "" || textBoxTitle.Text == "" || textBoxDes.Text == ""
				|| suggestAuthor.Text == "" || comboYear.SelectedValue == null )
				return "Điền thông tin các trường";
			//check length
			if ( textBoxTitle.Text.Length > 100 )
				return "Tựa sách không quá 100 kí tự";
			if ( textBoxDes.Text.Length > 5000 )
				return "Miêu tả không quá 5000 ký tự";
			return "";
		}

		private async void SuggestTextChanged ( AutoSuggestBox sender , AutoSuggestBoxTextChangedEventArgs args )
		{
			string result = await RestAPI.SendJson ( suggestAuthor.Text , RestAPI.phpAddress , "GetAuthor" );
			dynamic json = JArray.Parse ( result );
			var l = new ObservableCollection<object> ();
			for ( int i = 0 ; i < json.Count ; i++ )
			{
				l.Add ( new
				{
					authorName = json[i].authorName
				} );
			}
			suggestAuthor.ItemsSource = l;
		}
	}
}
