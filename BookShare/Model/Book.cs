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
	class Book
	{
		[JsonProperty]
		public string id { get; set; }

		[JsonProperty]
		public string title { get; set; }

		[JsonProperty]
		public string year { get; set; }

		[JsonProperty ( NullValueHandling = NullValueHandling.Ignore )]
		public string description { get; set; }

		public string image { get; set; }

		public Book ()
		{
			description = "";
		}

		public void SetImageLink ()
		{
			this.image = RestAPI.serverAddress + "cover/" + id + ".jpg";
		}
	}
}
