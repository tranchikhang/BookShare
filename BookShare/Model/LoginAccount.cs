using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShare.Model
{
	class LoginAccount
	{
		[JsonProperty ( "user" )]
		private string user;

		[JsonProperty ( "password" )]
		private string password;

		public LoginAccount ( string user , string password )
		{
			this.user = user;
			this.password = password;
		}
	}
}
