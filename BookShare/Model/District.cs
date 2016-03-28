using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShare.Model
{
	class District
	{
		[JsonProperty ( PropertyName = "districtId" )]
		public string id { get; set; }
		[JsonProperty ( PropertyName = "districtName" )]
		public string name { get; set; }
		public string cityId { get; set; }
	}
}
