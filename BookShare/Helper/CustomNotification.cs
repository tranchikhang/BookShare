using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Notifications;
using Windows.UI.Popups;

namespace BookShare.Helper
{
	public class CustomNotification
	{
		private static MessageDialog messageDialog;

		public async static void ShowDialogMessage
			( string title = "Lỗi" , string content = "Đã xảy ra lỗi, thử lại sau" )
		{
			if ( messageDialog == null )
				messageDialog = new MessageDialog ( "" );
			messageDialog.Title = title;
			messageDialog.Content = content;
			await messageDialog.ShowAsync ();
		}

		public static void ShowNotification ( string text )
		{
			var notificationXml = ToastNotificationManager.GetTemplateContent ( ToastTemplateType.ToastText01 );
			var toeastElement = notificationXml.GetElementsByTagName ( "text" );
			toeastElement[0].AppendChild ( notificationXml.CreateTextNode ( text ) );
			var toastNotification = new ToastNotification ( notificationXml );
			ToastNotificationManager.CreateToastNotifier ().Show ( toastNotification );
		}
	}
}
