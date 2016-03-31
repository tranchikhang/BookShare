using Newtonsoft.Json;

namespace BookShare.Model.Misc
{
	[JsonObject ( MemberSerialization.OptIn )]
	class Statistic
	{
		[JsonProperty ( PropertyName = "numberOfBook" )]
		public int numberOfBook { get; set; }

		public string bookInfo { get; set; }

		[JsonProperty ( PropertyName = "numberOfUser" )]
		public int numberOfUser { get; set; }

		public string userInfo { get; set; }

		[JsonProperty ( PropertyName = "numberOfMessage" )]
		public int numberOfMessage { get; set; }

		public string messageInfo { get; set; }

		[JsonProperty ( PropertyName = "numberOfRequest" )]
		public int numberOfRequest { get; set; }

		public string requestInfo { get; set; }

		public void AddString()
		{
			bookInfo = numberOfBook.ToString () + " sách đã chia sẻ";
			userInfo = numberOfUser.ToString () + " thành viên";
			messageInfo = numberOfMessage.ToString () + " tin nhắn";
			requestInfo = numberOfRequest.ToString () + " yêu cầu";
		}
	}
}
