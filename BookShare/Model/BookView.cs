﻿using BookShare.Helper;
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

		[JsonProperty ( "bookid" )]
		public string bookid { get; set; }

		[JsonProperty ( "authorid" )]
		public string authorid { get; set; }

		public string image { get; set; }

		public BookView ( string bookid , string book , string authorid , string author )
		{
			this.bookid = bookid;
			this.book = book;
			this.authorid = authorid;
			this.author = author;
		}

		public void SetImageLink ()
		{
			this.image = RestAPI.serverAdress + "cover/" + bookid + ".jpg";
		}
	}
}
