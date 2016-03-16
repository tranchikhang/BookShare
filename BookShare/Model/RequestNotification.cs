using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShare.Model
{
	[JsonObject ( MemberSerialization.OptIn )]
	class RequestNotification
	{
		[JsonProperty]
		public string requestId { get; set; }
		[JsonProperty]
		public bool requestAccepted { get; set; }
		[JsonProperty]
		public string userId { get; set; }
		[JsonProperty]
		public string userAccount { get; set; }
		[JsonProperty]
		public string bookId { get; set; }
		[JsonProperty]
		public string bookTitle { get; set; }
		[JsonProperty]
		public string content { get; set; }

		public void SetContent ()
		{
			if ( requestAccepted )
			{
				content = " đã đồng ý yêu cầu mượn sách " + bookTitle + " của bạn.";
			}
			else
				content = " đã từ chối yêu cầu mượn sách " + bookTitle + " của bạn.";
		}
	}
}
