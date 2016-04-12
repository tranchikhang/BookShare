using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;

namespace BookShare.Model.Control
{
	class CustomMessageDialog
	{
		public static MessageDialog NewCustomMessageDialog ( string content , string title )
		{
			var dialog =
				new MessageDialog ( content , title );

			dialog.Commands.Add ( new UICommand ( "Có" ) { Id = 0 } );
			dialog.Commands.Add ( new UICommand ( "Không" ) { Id = 1 } );

			dialog.DefaultCommandIndex = 0;
			dialog.CancelCommandIndex = 1;
			return dialog;
		}

	}
}
