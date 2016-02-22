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
		public string email { get; set; }

		[JsonProperty]
		public string account { get; set; }

		[JsonProperty]
		public string fullname { get; set; }

		[JsonProperty]
		public string address { get; set; }

		[JsonProperty]
		public string districtId { get; set; }

		[JsonProperty]
		public string cityId { get; set; }
	}
}
