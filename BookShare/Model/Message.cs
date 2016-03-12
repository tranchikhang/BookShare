using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShare.Model
{
	[JsonObject ( MemberSerialization.OptIn )]
	class Message
	{
		[JsonProperty]
		public string fromUserId { get; set; }
		[JsonProperty]
		public string toUserId { get; set; }
		[JsonProperty]
		public string content { get; set; }
		[JsonProperty]
		public bool isRead { get; set; }
		[JsonProperty]
		public string datetime { get; set; }

		public bool isSender { get; set; }

		public Message ()
		{
			//
		}

		public Message ( string fromUserId , string toUserId , string content )
		{
			this.fromUserId = fromUserId;
			this.toUserId = toUserId;
			this.content = content;
			isRead = true;
		}
	}
}
