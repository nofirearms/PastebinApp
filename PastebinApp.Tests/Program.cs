using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace PastebinApp.Tests
{
	class Program
	{
		private static HttpClient _client = new HttpClient();
		private static string _userKey = string.Empty;
		private static readonly string _devKey = "j_ITFRMgE7i17pA649YSBQc9_tn6OaDZ";

		static void Main(string[] args)
		{
			DoStuff();

			Console.WriteLine("Enter paste key:");

			var paste_key = Console.ReadLine();

			GetRaw(paste_key).Wait();

			Console.ReadLine();

		}

		public static async void DoStuff()
		{
			//await GetUserKeyAsync(); 
			//await GetPastesAsync();
			//await GetUserInfoAsync();
			//await CreatePasteAsync();
			await Test1();
		}

		public static async Task GetRaw(string key)
		{
			await GetRawPasteAsync(key);
		}

		//curl -X POST -d 'api_dev_key=j_ITFRMgE7i17pA649YSBQc9_tn6OaDZ' -d 'api_user_name=a_users_username' -d 'api_user_password=a_users_password' "https://pastebin.com/api/api_login.php"
		public static async Task GetUserKeyAsync()
		{
			
			var content = new Dictionary<string, string>();

			content.Add("api_dev_key", _devKey);
			content.Add("api_user_name", "Superudar");
			content.Add("api_user_password", "0YouDontExist1");

			var response = await _client.PostAsync(new Uri("https://pastebin.com/api/api_login.php?"), new FormUrlEncodedContent(content));
			
			var result = await response.Content.ReadAsStringAsync();
			
			_userKey = result;

			Console.WriteLine(result); 
		}

		//curl -X POST -d 'api_dev_key=j_ITFRMgE7i17pA649YSBQc9_tn6OaDZ' -d 'api_user_key=YOUR API USER KEY' -d 'api_option=list' -d 'api_results_limit=100' "https://pastebin.com/api/api_post.php"
		public static async Task GetPastesAsync()
		{
			var content = new Dictionary<string, string>();

			content.Add("api_dev_key", _devKey);
			content.Add("api_user_key", _userKey);
			content.Add("api_option", "list");

			var response = await _client.PostAsync(new Uri("https://pastebin.com/api/api_post.php?"), new FormUrlEncodedContent(content));

			var result = await response.Content.ReadAsStringAsync();

			result = $"<pastes>{result}</pastes>";

			var xml = new XmlDocument();

			xml.LoadXml(result);

			var json = JsonConvert.SerializeObject(xml.FirstChild); 

			Console.WriteLine(json);
		}

		public static async Task GetUserInfoAsync()
		{
			var content = new Dictionary<string, string>();

			content.Add("api_dev_key", _devKey);
			content.Add("api_user_key", _userKey);
			content.Add("api_option", "userdetails");

			var response = await _client.PostAsync(new Uri("https://pastebin.com/api/api_post.php?"), new FormUrlEncodedContent(content));

			var result = await response.Content.ReadAsStringAsync();

			var xml = new XmlDocument();

			xml.LoadXml(result);

			var json = JsonConvert.SerializeXmlNode(xml);

			Console.WriteLine(json);
		}

		public static async Task CreatePasteAsync() 
		{
			var content = new Dictionary<string, string>();

			content.Add("api_dev_key", _devKey);
			content.Add("api_user_key", _userKey);
			content.Add("api_option", "paste");
			content.Add("api_paste_name", "stuff");
			content.Add("api_paste_code", DateTime.Now.ToLongTimeString());
			content.Add("api_paste_private", "2");

			var response = await _client.PostAsync(new Uri("https://pastebin.com/api/api_post.php?"), new FormUrlEncodedContent(content));

			var result = await response.Content.ReadAsStringAsync();

			Console.WriteLine(result);
		}

		//curl -X POST -d 'api_dev_key=j_ITFRMgE7i17pA649YSBQc9_tn6OaDZ' -d 'api_user_key=YOUR API USER KEY' -d 'api_option=show_paste' -d 'api_paste_key=API PASTE KEY' "https://pastebin.com/api/api_post.php"

		public static async Task GetRawPasteAsync(string apiPasteKey)
		{
			var content = new Dictionary<string, string>();

			content.Add("api_dev_key", _devKey);
			content.Add("api_user_key", _userKey);
			content.Add("api_option", "show_paste");
			content.Add("api_paste_key", apiPasteKey);

			var response = await _client.PostAsync(new Uri("https://pastebin.com/api/api_post.php"), new FormUrlEncodedContent(content));

			var result = await response.Content.ReadAsStringAsync();

			Console.WriteLine(result);
		}


		public static async Task Test1()
		{
			var content = new Dictionary<string, string>();

			var response = await _client.GetAsync("https://superuser.bsite.net/weatherforecast");

			var result = await response.Content.ReadAsStringAsync();

			Console.WriteLine(result);
		}

		//public static async void DoStuff()
		//{

		//	var content = new Dictionary<string, string>();

		//	content.Add("api_dev_key", "j_ITFRMgE7i17pA649YSBQc9_tn6OaDZ");
		//	content.Add("api_paste_code", "some kind of shit");
		//	content.Add("api_option", "paste");
		//	content.Add("api_expire_date", "10M");

		//	var response = await _client.PostAsync(new Uri("https://pastebin.com/api/api_post.php?"), new FormUrlEncodedContent(content));

		//	var result = await response.Content.ReadAsStringAsync();
		//}
	}


}

//https://pastebin.com/api/api_post.php?api_dev_key=j_ITFRMgE7i17pA649YSBQc9_tn6OaDZ&api_paste_code=12323&api_option=paste&api_expire_date=10M
//api_user_name=a_users_username' -d 'api_user_password=a_users_password'