﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Web.Http;

namespace BookShare.Helper
{
	class RestAPI
	{
		public static string phpAdress = "http://localhost/BookShare/api/v1/";
		private static HttpClient httpClient;
		private static HttpResponseMessage response;
		static public async Task<string> SendJson(object data , string address , string key)
		{
			string json = JsonConvert.SerializeObject ( data );
			//create dictionary
			var dict = new Dictionary<string , string> ();
			dict[key] = json;

			httpClient = new HttpClient ();
			response = new HttpResponseMessage ();
			string responseText = "";

			//check address
			Uri resourceUri;
			if ( !Uri.TryCreate ( address.Trim () , UriKind.Absolute , out resourceUri ) )
			{
				return "Invalid URI, please re-enter a valid URI";
			}
			if ( resourceUri.Scheme != "http" && resourceUri.Scheme != "https" )
			{
				return "Only 'http' and 'https' schemes supported. Please re-enter URI";
			}

			//post
			try
			{
				response = await httpClient.PostAsync ( resourceUri , new HttpFormUrlEncodedContent ( dict ) );
				response.EnsureSuccessStatusCode ();
				responseText = await response.Content.ReadAsStringAsync ();
			}
			catch (Exception ex)
			{
				// Need to convert int HResult to hex string
				responseText = "Error = " + ex.HResult.ToString ( "X" ) + "  Message: " + ex.Message;
			}
			httpClient.Dispose ();
			return responseText;
		}
	}
}
