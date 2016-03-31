using BookShare.Helper;
using Newtonsoft.Json;
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
		[JsonProperty ( PropertyName = "postId" )]
		public string postId { get; set; }

		[JsonProperty ( PropertyName = "bookId" )]
		public string bookId { get; set; }

		[JsonProperty ( PropertyName = "title" )]
		public string title { get; set; }

		[JsonProperty]
		public string image { get; set; }

		[JsonProperty ( PropertyName = "author" )]
		public string author { get; set; }

		[JsonProperty ( PropertyName = "requests" )]
		public ObservableCollection<Request> requests { get; set; }

		[JsonProperty ( PropertyName = "posts" )]
		public ObservableCollection<Post> posts { get; set; }

		public void SetImageLink ()
		{
			//set book cover link
			image = RestAPI.publicApiAddress + "cover/" + bookId + ".jpg";
			//set user ava link
			SetUserAva ();
		}

		private void SetUserAva ()
		{
			if ( requests != null )
			{
				foreach ( Request r in requests )
				{
					r.user.SetAva ();
				}
			}
			if ( posts != null )
			{
				foreach ( Post p in posts )
				{
					p.user.SetAva ();
				}
			}
		}
	}
}
