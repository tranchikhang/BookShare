using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace BookShare.AppPage
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class StartPage : Page
	{
		public StartPage ()
		{
			this.InitializeComponent ();
		}

		private void RegisterClick ( object sender , RoutedEventArgs e )
		{
			this.Frame.Navigate ( typeof ( Register ) );
		}

		private void LoginClick ( object sender , RoutedEventArgs e )
		{
			this.Frame.Navigate ( typeof ( Login ) );
		}
	}
}
