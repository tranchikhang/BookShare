using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.ViewManagement;

namespace BookShare.Helper
{
	class WindowResize
	{
		public static void Resize( int width, int height)
		{
			//default size
			ApplicationView.PreferredLaunchViewSize = new Size ( width , height );
			ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;
			//minimum resize
			ApplicationView.GetForCurrentView ().SetPreferredMinSize ( new Size ( width , height ) );
		}
	}
}
