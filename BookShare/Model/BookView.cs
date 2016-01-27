using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShare.Model
{
	class BookView
	{
		[JsonProperty ( "book" )]
		public string book { get; set; }

		[JsonProperty ( "author" )]
		public string author { get; set; }

		public BookView ( string book , string author )
		{
			this.book = book;
			this.author = author;
		}
	}
}
