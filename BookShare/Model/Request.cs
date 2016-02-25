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
		[JsonProperty]
		public string requestId { get; set; }
		[JsonProperty]
		public string requestUserAccount { get; set; }
		[JsonProperty]
		public string requestUserAva { get; set; }
		[JsonProperty]
		public string requestUserId { get; set; }
		[JsonProperty]
		public string requestMessage { get; set; }
	}
}
