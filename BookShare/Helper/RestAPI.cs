﻿using BookShare.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Web.Http;

namespace BookShare.Helper
{
	class RestAPI
	{
		public static string publicApiAddress = "http://localhost/SlimDemo/public/";
		private static HttpClient httpClient;
		private static HttpResponseMessage response;
		public enum ResponseStatus { OK, Empty, Failed };

		static public async Task<string> SendGetRequest ( string address )
		{
			var httpFilter = new Windows.Web.Http.Filters.HttpBaseProtocolFilter ();
			httpFilter.CacheControl.ReadBehavior =
				Windows.Web.Http.Filters.HttpCacheReadBehavior.MostRecent;
			httpClient = new HttpClient ( httpFilter );
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

			//get
			try
			{
				response = await httpClient.GetAsync ( resourceUri );
				response.EnsureSuccessStatusCode ();
				responseText = await response.Content.ReadAsStringAsync ();
			}
			catch ( Exception ex )
			{
				// Need to convert int HResult to hex string
				responseText = "Error = " + ex.HResult.ToString ( "X" ) + "  Message: " + ex.Message;
			}
			httpClient.Dispose ();
			return responseText;
		}

		static public async Task<string> SendPostRequest ( object data , string address )
		{
			string json = JsonConvert.SerializeObject ( data );
			//string tokenJson = JsonConvert.SerializeObject ( UserData.token );
			//create dictionary
			var dict = new Dictionary<string , string> ();
			dict["data"] = json;

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
			catch ( Exception ex )
			{
				// Need to convert int HResult to hex string
				responseText = "Error = " + ex.HResult.ToString ( "X" ) + "  Message: " + ex.Message;
			}
			httpClient.Dispose ();
			return responseText;
		}

		static public async Task<string> SendPutRequest ( object data , string address )
		{
			string json = JsonConvert.SerializeObject ( data );
			//string tokenJson = JsonConvert.SerializeObject ( UserData.token );
			//create dictionary
			var dict = new Dictionary<string , string> ();
			dict["data"] = json;

			var httpFilter = new Windows.Web.Http.Filters.HttpBaseProtocolFilter ();
			httpFilter.CacheControl.ReadBehavior =
				Windows.Web.Http.Filters.HttpCacheReadBehavior.MostRecent;
			httpClient = new HttpClient ( httpFilter );
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
				response = await httpClient.PutAsync ( resourceUri , new HttpFormUrlEncodedContent ( dict ) );
				response.EnsureSuccessStatusCode ();
				responseText = await response.Content.ReadAsStringAsync ();
			}
			catch ( Exception ex )
			{
				// Need to convert int HResult to hex string
				responseText = "Error = " + ex.HResult.ToString ( "X" ) + "  Message: " + ex.Message;
			}
			httpClient.Dispose ();
			return responseText;
		}
	}
}
