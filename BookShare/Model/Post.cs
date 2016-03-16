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
	class Post
	{
		[JsonProperty ( PropertyName = "postId" )]
		public string id { get; set; }

		[JsonProperty ( PropertyName = "isSendable" )]
		public bool isSendable { get; set; }

		[JsonProperty ( PropertyName = "user" )]
		public User user { get; set; }

		[JsonProperty ( PropertyName = "book" )]
		public Book book { get; set; }
	}
}
