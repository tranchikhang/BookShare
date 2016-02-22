using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Notifications;

namespace BookShare.Helper
{
	class CustomNotification
	{
		public static void DisplayNotification (string text)
		{
			var notificationXml = ToastNotificationManager.GetTemplateContent ( ToastTemplateType.ToastText01 );
			var toeastElement = notificationXml.GetElementsByTagName ( "text" );
			toeastElement[0].AppendChild ( notificationXml.CreateTextNode ( text ) );
			var toastNotification = new ToastNotification ( notificationXml );
			ToastNotificationManager.CreateToastNotifier ().Show ( toastNotification );
		}
	}
}
