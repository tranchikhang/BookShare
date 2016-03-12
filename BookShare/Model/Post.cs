using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShare.Model
{
	[JsonObject ( MemberSerialization.OptIn )]
	class Post
	{
		[JsonProperty]
		public string postId { get; set; }
		[JsonProperty]
		public string postUserAccount { get; set; }
		[JsonProperty]
		public string postUserAva { get; set; }
		[JsonProperty]
		public string postUserId { get; set; }
		[JsonProperty]
		public string postMessage { get; set; }
	}
}
