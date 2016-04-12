using BookShare.Helper;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace BookShare.AppPage
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class MainPage : Page
	{
		public MainPage ()
		{
			this.InitializeComponent ();
		}

		protected override void OnNavigatedTo ( NavigationEventArgs e )
		{
			base.OnNavigatedTo ( e );
			MainSplitView.IsPaneOpen = false;
			NavigationMethod.SetMainFrame ( mainFrame );
			NavigationMethod.SetTopFrame ( Frame );
			if ( MainSplitView.Content != null )
			{
				mainFrame.Navigate ( typeof ( GreetingPage ) );
				textBlockTitle.Text = "Trang chủ";
			}
		}

		private void HomeClick ( object sender , RoutedEventArgs e )
		{
			if ( MainSplitView.Content != null )
			{
				MainSplitView.IsPaneOpen = false;
				textBlockTitle.Text = "Trang chủ";
				mainFrame.Navigate ( typeof ( GreetingPage ) );
			}
		}

		private void SettingClick ( object sender , RoutedEventArgs e )
		{
			if ( MainSplitView.Content != null )
			{
				MainSplitView.IsPaneOpen = false;
				textBlockTitle.Text = "Cài đặt";
				mainFrame.Navigate ( typeof ( SettingPage ) );
			}
		}

		private void RequestListClick ( object sender , RoutedEventArgs e )
		{
			if ( MainSplitView.Content != null )
			{
				MainSplitView.IsPaneOpen = false;
				textBlockTitle.Text = "Yêu cầu";
				mainFrame.Navigate ( typeof ( RequestList ) );
			}
		}

		private void BookShelfClick ( object sender , RoutedEventArgs e )
		{

			if ( MainSplitView.Content != null )
			{
				MainSplitView.IsPaneOpen = false;
				textBlockTitle.Text = "Tủ sách";
				mainFrame.Navigate ( typeof ( BookShelf ) );
			}
		}

		private void MessageListClick ( object sender , RoutedEventArgs e )
		{
			if ( MainSplitView.Content != null )
			{
				MainSplitView.IsPaneOpen = false;
				textBlockTitle.Text = "Tin nhắn";
				mainFrame.Navigate ( typeof ( MessagePage ) );
			}
		}

		private void InfoClick ( object sender , RoutedEventArgs e )
		{
			if ( MainSplitView.Content != null )
			{
				textBlockTitle.Text = "Giới thiệu";
				MainSplitView.IsPaneOpen = false;
				mainFrame.Navigate ( typeof ( About ) );
			}
		}

		private void NotificationClick ( object sender , RoutedEventArgs e )
		{
			if ( MainSplitView.Content != null )
			{
				textBlockTitle.Text = "Thông báo";
				MainSplitView.IsPaneOpen = false;
				mainFrame.Navigate ( typeof ( NotificationPage ) );
			}
		}

		private void HamburgerClick ( object sender , Windows.UI.Xaml.Input.TappedRoutedEventArgs e )
		{
			MainSplitView.IsPaneOpen = !MainSplitView.IsPaneOpen;
		}
	}
}
