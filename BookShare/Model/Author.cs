using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShare.Model
{
	[JsonObject ( MemberSerialization.OptIn )]
	class Author
	{
		[JsonProperty ( PropertyName = "authorId" )]
		public string id { get; set; }

		[JsonProperty ( PropertyName = "author" )]
		public string author { get; set; }

		[JsonProperty]
		private string description { get; set; }
	}
}
