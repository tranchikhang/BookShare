using BookShare.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShare.Helper
{
	class JsonHelper
	{
		public static RestAPI.ResponseStatus IsRequestSucceed ( string r )
		{
			try
			{
				Dictionary<string , string> dict = JsonConvert.DeserializeObject<Dictionary<string , string>> ( r );
				if ( dict["status"] == "200" )
				{
					return RestAPI.ResponseStatus.OK;
				}
				if ( dict["status"] == "204" )
				{
					return RestAPI.ResponseStatus.Empty;
				}
				else
					return RestAPI.ResponseStatus.Failed;
			}
			catch ( Exception ex )
			{
				return RestAPI.ResponseStatus.Failed;
			}
		}
		public static string DecodeJson ( string r )
		{
			Dictionary<string , string> dict = JsonConvert.DeserializeObject<Dictionary<string , string>> ( r );
			return dict["data"];
		}

		public static User ConvertToUser ( string r )
		{
			User user = JsonConvert.DeserializeObject<User> ( r );
			return user;
		}

		public static ObservableCollection<User> ConvertToUsers ( string r )
		{
			ObservableCollection<User> users = new ObservableCollection<User> ();
			users = JsonConvert.DeserializeObject<ObservableCollection<User>> ( r );
			return users;
		}

		public static ObservableCollection<Post> ConvertToPosts ( string r )
		{
			ObservableCollection<Post> posts = new ObservableCollection<Post> ();
			posts = JsonConvert.DeserializeObject<ObservableCollection<Post>> ( r );
			return posts;
		}

		public static Book ConvertToBook ( string r )
		{
			Book book = JsonConvert.DeserializeObject<Book> ( r );
			return book;
		}

		public static ObservableCollection<District> ConvertToDistricts ( string r )
		{
			ObservableCollection<District> districts = new ObservableCollection<District> ();
			districts = JsonConvert.DeserializeObject<ObservableCollection<District>> ( r );
			return districts;
		}

		public static ObservableCollection<City> ConvertToCities ( string r )
		{
			ObservableCollection<City> cities = new ObservableCollection<City> ();
			cities = JsonConvert.DeserializeObject<ObservableCollection<City>> ( r );
			return cities;
		}

		public static ObservableCollection<Message> ConvertToMessages ( string r )
		{
			ObservableCollection<Message> messages = new ObservableCollection<Message> ();
			messages = JsonConvert.DeserializeObject<ObservableCollection<Message>> ( r );
			return messages;
		}

		public static ObservableCollection<Conversation> ConverToConversations ( string r )
		{
			ObservableCollection<Conversation> conversations = new ObservableCollection<Conversation> ();
			conversations = JsonConvert.DeserializeObject<ObservableCollection<Conversation>> ( r );
			return conversations;
		}

		public static ObservableCollection<Book> ConvertToBooks ( string r )
		{
			ObservableCollection<Book> books = new ObservableCollection<Book> ();
			books = JsonConvert.DeserializeObject<ObservableCollection<Book>> ( r );
			return books;
		}
	}
}
