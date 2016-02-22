using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace BookShare.Model
{
	class AppSettings
	{
		public const string keyId = "Id";
		public const string keyFirstOpen = "IsFirstOpen";
		public const string keyToken = "Token";

		public ApplicationDataContainer localSettings;

		public AppSettings ()
		{
			localSettings = ApplicationData.Current.LocalSettings;
		}

		public void Add ( string key , object value )
		{
			if ( localSettings.Values[key] == null )
			{
				localSettings.Values[key] = value;
			}
		}

		public bool Contain ( string key )
		{
			if ( localSettings.Values[key] == null )
			{
				return false;
			}
			return false;
		}

		public void Update ( string key , object value )
		{
			if ( localSettings.Values[key] != null )
			{
				if ( localSettings.Values[key] != value )
					localSettings.Values[key] = value;
			}
		}

		public void Remove ( string key )
		{
			if ( localSettings.Values[key] != null )
				localSettings.Values.Remove ( key );
		}

		public T GetValueOrDefault<T> ( string key , T defaultValue )
		{
			T value;
			if ( localSettings.Values[key] != null )
			{
				value = ( T ) localSettings.Values[key];
			}
			else
			{
				value = defaultValue;
			}
			return value;
		}
	}
}
