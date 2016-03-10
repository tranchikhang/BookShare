﻿using BookShare.Helper;
using BookShare.Model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Streams;
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
	public sealed partial class AddNewBook : Page
	{
		public AddNewBook ()
		{
			this.InitializeComponent ();
			AddGenre ();
			AddYear ();
		}

		private void AddYear ()
		{
			for ( int i = 1950 ; i < 2020 ; i++ )
			{
				comboYear.Items.Add ( i );
			}
		}

		private StorageFile file;

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
			if ( textBlockFile.Text != "" && textBoxTitle.Text != "" && textBoxDes.Text != ""
				&& suggestAuthor.Text != "" && comboYear.SelectedValue != null )
			{
				//send request
				string imageString = await ImageUpload.StorageFileToBase64 ( file );
				dynamic sendJson = new
				{
					title = textBoxTitle.Text ,
					author = suggestAuthor.Text ,
					year = comboYear.SelectedValue ,
					genreId = comboBoxGenre.SelectedValue ,
					description = textBoxDes.Text ,
					image = imageString ,
					userId = UserData.id
				};
				string result = await RestAPI.SendJson ( sendJson , RestAPI.phpAddress , "AddNewBook" );
				dynamic json = JObject.Parse ( result );
				string status = json.status;
				string message = json.message;
				if ( status == "200" )
				{
					CustomNotification.ShowNotification ( message );
					Frame.Navigate ( typeof ( MainPage ) );
				}
			}
			else
			{
				CustomNotification.ShowNotification ( "Kiểm tra lại thông tin đã nhập" );
			}
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
					//authorId = json[i].authorId ,
					authorName = json[i].authorName
				} );
			}
			suggestAuthor.ItemsSource = l;
		}
	}
}