using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace BookShare.Model
{
	class City
	{
		[JsonProperty ( PropertyName = "cityId" )]
		public string id { get; set; }
		[JsonProperty ( PropertyName = "cityName" )]
		public string name { get; set; }
		[JsonProperty ( PropertyName = "districts" )]
		public ObservableCollection<District> districts { get; set; }
	}
}
