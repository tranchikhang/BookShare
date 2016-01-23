using Newtonsoft.Json;

namespace BookShare.Model
{
	class RegisterAccount
	{
		[JsonProperty ( "user" )]
		private string user;

		[JsonProperty ( "email" )]
		private string email;

		[JsonProperty ( "fullname" )]
		private string fullname;

		[JsonProperty ( "password" )]
		private string password;

		public RegisterAccount(string user, string email, string fullname, string password)
		{
			this.user = user;
			this.email = email;
			this.fullname = fullname;
			this.password = password;
		}
	}
}
