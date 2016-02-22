using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShare.Model
{
	class UserData
	{
		public static string id;
		public static AppSettings settings;
		public static string token;

		public static bool LoadTokenAndIdFromSetting()
		{
			token = settings.GetValueOrDefault ( AppSettings.keyToken , "" );
			if ( token == "" )
				return false;
			id = settings.GetValueOrDefault ( AppSettings.keyId , "" );
			return true;
		}
	}
}
