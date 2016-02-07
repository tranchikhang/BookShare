using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShare.Model
{
	[JsonObject ( MemberSerialization.OptIn )]
	class User
	{
		[JsonProperty]
		public string id { get; set; }

		[JsonProperty]
		public string account { get; set; }

		[JsonProperty]
		public string fullname { get; set; }
	}
}
