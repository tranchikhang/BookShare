﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace BookShare.Model
{
	class AppSettings
	{
		public AppSettings()
		{
			var localSettings = ApplicationData.Current.LocalSettings;
		}
	}
}
