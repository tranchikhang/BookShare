﻿using Newtonsoft.Json;
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
		[JsonProperty]
		public string id { get; set; }

		[JsonProperty]
		public string name { get; set; }

		[JsonProperty]
		private string description { get; set; }
	}
}
