﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShare.Model
{
	[JsonObject ( MemberSerialization.OptIn )]
	class PostedBook
	{
		[JsonProperty]
		public string postId { get; set; }
		[JsonProperty]
		public string bookId { get; set; }
		[JsonProperty]
		public string title { get; set; }
		[JsonProperty]
		public string image { get; set; }
		[JsonProperty]
		public string author { get; set; }
		[JsonProperty]
		public ObservableCollection<Request> requests { get; set; }
		[JsonProperty]
		public ObservableCollection<Post> posts { get; set; }
	}
}
