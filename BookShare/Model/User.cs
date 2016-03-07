using BookShare.Helper;
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
		public string id { get; set; }

		[JsonProperty]
		public string email { get; set; }

		[JsonProperty]
		public string account { get; set; }

		[JsonProperty]
		public string fullname { get; set; }

		public string fullAddress { get; set; }

		[JsonProperty]
		public string address { get; set; }

		[JsonProperty]
		public string districtId { get; set; }

		[JsonProperty]
		public string district { get; set; }

		[JsonProperty]
		public string cityId { get; set; }

		[JsonProperty]
		public string city { get; set; }

		[JsonProperty]
		public string ava { get; set; }

		public string defaultAva = RestAPI.serverAddress + "resources/images/defaultAva.png";
	}
}
