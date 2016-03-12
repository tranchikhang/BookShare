using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;

namespace BookShare.Helper
{
	class ImageUpload
	{
		public static async Task<string> StorageFileToBase64 ( StorageFile file )
		{
			string Base64String = "";

			if ( file != null )
			{
				IRandomAccessStream fileStream = await file.OpenAsync ( FileAccessMode.Read );
				var reader = new DataReader ( fileStream.GetInputStreamAt ( 0 ) );
				await reader.LoadAsync ( ( uint ) fileStream.Size );
				byte[] byteArray = new byte[fileStream.Size];
				reader.ReadBytes ( byteArray );
				Base64String = Convert.ToBase64String ( byteArray );
			}

			return Base64String;
		}
	}
}
