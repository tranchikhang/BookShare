using Newtonsoft.Json;

namespace BookShare.Model
{
	[JsonObject ( MemberSerialization.OptIn )]
	class Post
	{
		[JsonProperty ( PropertyName = "postId" )]
		public string id { get; set; }

		[JsonProperty ( PropertyName = "isSendable" )]
		public int isSendable { get; set; }

		[JsonProperty ( PropertyName = "user" )]
		public User user { get; set; }

		[JsonProperty ( PropertyName = "book" )]
		public Book book { get; set; }

		[JsonProperty ( PropertyName = "isAvailable" )]
		public bool isAvailable { get; set; }
	}
}
