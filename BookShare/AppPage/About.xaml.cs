﻿using BookShare.Helper;
using BookShare.Model.Misc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
	public sealed partial class About : Page
	{
		public About ()
		{
			this.InitializeComponent ();
			LoadData ();
		}
		private Statistic s;

		private async void LoadData ()
		{
			string result =
				await RestAPI.SendGetRequest ( RestAPI.publicApiAddress + "statistic/" );
			if ( JsonHelper.IsRequestSucceed ( result ) == RestAPI.ResponseStatus.OK )
			{
				string data = JsonHelper.DecodeJson ( result );
				s = JsonHelper.ConvertToStatistic ( data );
				s.AddString ();
				gridNumbers.DataContext = s;
			}
		}

		private async void SendEmailTap ( object sender , TappedRoutedEventArgs e )
		{
			await Windows.System.Launcher.LaunchUriAsync ( new Uri ( "mailto:tranchikhang@outook.com" ) );
		}
	}
}