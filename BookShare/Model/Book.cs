using BookShare.Helper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShare.Model
{
	class Book
	{
		[JsonProperty ( "id" )]
		public string id { get; set; }

		[JsonProperty ( "title" )]
		public string title { get; set; }

		[JsonProperty ( "year" )]
		public string year { get; set; }

		[JsonProperty ( "description" )]
		public string description { get; set; }

		public string image { get; set; }

		public void SetImageLink ()
		{
			this.image = RestAPI.serverAdress + "cover/" + id + ".jpg";
		}

		public void CheckNullDescription ()
		{
			if ( description == null )
			{
				description = "";
			}
		}
	}
}
