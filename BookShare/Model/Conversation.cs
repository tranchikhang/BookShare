using BookShare.Helper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShare.Model
{
	[JsonObject ( MemberSerialization.OptIn )]
	class Conversation
	{
		[JsonProperty ( PropertyName = "allMessage" )]
		public ObservableCollection<Message> messages { get; set; }

		[JsonProperty ( PropertyName = "userAccount" )]
		public string userAccount { get; set; }

		[JsonProperty ( PropertyName = "userId" )]
		public string userId { get; set; }

		[JsonProperty ( PropertyName = "isAvaExist" )]
		public bool isAvaExist { get; set; }

		public string userAva { get; set; }

		public bool isNewMessage { get; set; }
		public string lastMessage { get; set; }

		public void SetLastMessage ()
		{
			lastMessage = messages.Last ().content;
		}

		public void CheckNewMessage ()
		{
			isNewMessage = LoopAllMessages ();
		}

		private bool LoopAllMessages ()
		{
			foreach ( Message m in messages )
			{
				if ( m.toUserId == UserData.id && m.isRead == false )
				{
					return true;
				}
			}
			return false;
		}

		public Conversation ()
		{
			//
		}

		public void SetAva ()
		{
			if ( isAvaExist )
			{
				userAva = RestAPI.publicApiAddress + "resources/images/users/" + userId + ".jpg";
			}
			else
				userAva = RestAPI.publicApiAddress + "resources/images/defaultAva.png";
		}
	}
}
