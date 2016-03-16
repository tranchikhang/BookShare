using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShare.Model
{
	[JsonObject ( MemberSerialization.OptIn )]
	class Request
	{
		[JsonProperty (PropertyName ="requestId")]
		public string id { get; set; }

		[JsonProperty ( PropertyName = "user" )]
		public User user { get; set; }

		[JsonProperty ( PropertyName = "requestMessage" )]
		public string message { get; set; }
	}
}
