using System;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace BookShare.Model.Control
{
	public sealed partial class MyNotification : UserControl
	{
		public MyNotification ()
		{
			InitializeComponent ();
		}

		private const string defaultContent = "Có lỗi, thử lại sau";

		public void Show ( bool isError , string content = defaultContent )
		{
			textBlockContent.Text = content;
			gridNotification.Visibility = Visibility.Visible;
			if ( isError )
			{
				//#F44336
				gridNotification.Background = new SolidColorBrush ( Color.FromArgb ( 255 , 244 , 67 , 54 ) );
			}
			else
			{
				//#4CAF50
				gridNotification.Background = new SolidColorBrush ( Color.FromArgb ( 255 , 76 , 175 , 80 ) );
			}
		}

		private void Dismiss ( object sender , RoutedEventArgs e )
		{
			textBlockContent.Text = "";
			gridNotification.Visibility = Visibility.Collapsed;
		}
	}
}
