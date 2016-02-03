using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShare.Model
{
	class Author
	{
		[JsonProperty ( "id" )]
		public string id { get; set; }

		[JsonProperty ( "name" )]
		public string name { get; set; }

		[JsonProperty ( "description" )]
		public string description { get; set; }
	}
}
