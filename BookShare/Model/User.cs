﻿using BookShare.Helper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShare.Model
{
	[JsonObject ( MemberSerialization.OptIn )]
	class User
	{
		[JsonProperty ( PropertyName = "userId" )]
		public string id { get; set; }

		[JsonProperty ( PropertyName = "userAccount" )]
		public string account { get; set; }

		[JsonProperty ( PropertyName = "userEmail" )]
		public string email { get; set; }

		[JsonProperty ( PropertyName = "userPassword" )]
		public string password { get; set; }

		[JsonProperty ( PropertyName = "userFullname" )]
		public string fullname { get; set; }

		[JsonProperty ( PropertyName = "userAddress" )]
		public string address { get; set; }

		public string fullAddress { get; set; }

		[JsonProperty ( PropertyName = "userDistrictId" )]
		public string districtId { get; set; }

		[JsonProperty ( PropertyName = "userDistrict" )]
		public string district { get; set; }

		[JsonProperty ( PropertyName = "userCityId" )]
		public string cityId { get; set; }

		[JsonProperty ( PropertyName = "userCity" )]
		public string city { get; set; }

		[JsonProperty ( PropertyName = "isAvaExist" )]
		public bool isAvaExist { get; set; }

		[JsonProperty ( PropertyName = "userToken" )]
		public string token { get; set; }

		public string ava { get; set; }

		public User ()
		{
			//
		}

		public void SetAva ()
		{
			if ( isAvaExist )
			{
				ava = RestAPI.publicApiAddress + "resources/images/users/" + id + ".jpg";
			}
			else
				ava = RestAPI.publicApiAddress + "resources/images/defaultAva.png";
		}

		public void SetAddress ()
		{
			if ( address == null || address == "" )
				fullAddress = "Quận " + district + ", " + city;
			else
				fullAddress = address + ", quận " + district + ", " + city;
		}
	}
}
