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
		[JsonProperty ( PropertyName = "bookId" )]
		public string id { get; set; }

		[JsonProperty ( PropertyName = "title" )]
		public string title { get; set; }

		[JsonProperty]
		public string year { get; set; }

		[JsonProperty ( NullValueHandling = NullValueHandling.Ignore )]
		public string description { get; set; }

		[JsonProperty ( PropertyName = "authorId" )]
		public string authorId { get; set; }

		[JsonProperty ( PropertyName = "author" )]
		public string author { get; set; }

		[JsonProperty ( PropertyName = "numberShared" )]
		public string numberShared { get; set; }

		[JsonProperty ( PropertyName = "isBookAdded" )]
		public bool isBookAdded { get; set; }

		public string image { get; set; }

		public Book ()
		{
			//
		}

		public void SetImageLink ()
		{
			//this.image = RestAPI.serverAddress + "cover/" + id + ".jpg";
			image = RestAPI.publicApiAddress + "cover/" + id + ".jpg";
		}
	}
}
