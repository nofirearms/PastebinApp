using Newtonsoft.Json;
using PastebinApp.Helpers;
using PastebinApp.Model;
using PastebinApp.Model.enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using Windows.Devices.Usb;
using Windows.Storage;

namespace PastebinApp.Services
{
	public class ApiService
	{

		private const string DEVKEY = "j_ITFRMgE7i17pA649YSBQc9_tn6OaDZ";

		private readonly HttpClient _client;
		private Login _login;

		public ApiService()
		{
			_client = new HttpClient();

			LoginAsync();
		}

		public bool IsLoggedIn => ApplicationData.Current.LocalSettings.Values.ContainsKey("Login");
		public string Username => _login?.Username ?? string.Empty;

		public async Task LoginAsync()
		{
			if (IsLoggedIn)
			{
                _login = JsonConvert.DeserializeObject<Login>(ApplicationData.Current.LocalSettings.Values["Login"].ToString());
            }
		}

		public void Logout()
		{
			_login = null;
			ApplicationData.Current.LocalSettings.Values.Remove("Login");
        }

		public async Task<Result> LoginAsync(string username, string password)
		{
			try
			{
				var content = new Dictionary<string, string>();

				content.Add("api_dev_key", DEVKEY);
				content.Add("api_user_name", username);
				content.Add("api_user_password", password);

				var response = await _client.PostAsync(new Uri("https://pastebin.com/api/api_login.php?"), new FormUrlEncodedContent(content));

				if (response is null) return new Result(false, "Server is not responding");

                if (!response.IsSuccessStatusCode) return new Result(false, await response.Content.ReadAsStringAsync());

                var result = await response.Content.ReadAsStringAsync();

				if (result is null) return new Result(false, "Server is not responding");

				var bad_request_pattern = @"Bad API request";

				if (!Regex.IsMatch(result, bad_request_pattern, RegexOptions.IgnoreCase))
				{
					_login = new Login
					{
						Username = username,
						Password = password,
						UserKey = result,
						LoginDate = DateTime.Now
					};

					ApplicationData.Current.LocalSettings.Values["Login"] = JsonConvert.SerializeObject(_login);

					return new Result(true, result);
				}

				return new Result(false, result);
			}
			catch(Exception e)
			{
				return new Result(false, e.Message);
			}


		}

		public async Task<Result> GetPastesAsync()
		{
			try
			{
				var content = new Dictionary<string, string>();

				content.Add("api_dev_key", DEVKEY);
				content.Add("api_user_key", _login.UserKey);
				content.Add("api_option", "list");

				var response = await _client.PostAsync(new Uri("https://pastebin.com/api/api_post.php?"), new FormUrlEncodedContent(content));

				if (response is null) return new Result(false, "Server is not responding");

                if (!response.IsSuccessStatusCode) return new Result(false, await response.Content.ReadAsStringAsync());

                var result = await response.Content.ReadAsStringAsync();

				if (result is null) return new Result(false, "Server is not responding");

				result = $"<pastes>{result}</pastes>";

				var xml = new XmlDocument();

				xml.LoadXml(result);

				var json = JsonConvert.SerializeXmlNode(xml);

				var pastes = JsonConvert.DeserializeObject<PastesRoot>(json);

				return new Result(true, pastes.Pastes.PastesList);
			}
			catch(Exception e)
			{
				return new Result(false, e.Message);
			}

		}

		public async Task<Result> CreatePasteAsync(string title, string text, string folder, Privacy privacy)
		{
			try
			{
				var content = new Dictionary<string, string>();

				content.Add("api_dev_key", DEVKEY);
				content.Add("api_user_key", _login.UserKey);
				content.Add("api_option", "paste");
				content.Add("api_paste_name", title);
				content.Add("api_paste_code", text);
				content.Add("api_folder_key", folder);
				content.Add("api_paste_private", ((int)privacy).ToString());

				var response = await _client.PostAsync(new Uri("https://pastebin.com/api/api_post.php?"), new FormUrlEncodedContent(content));

				if (response is null ) return new Result(false, "Server is not responding");

                if (!response.IsSuccessStatusCode) return new Result(false, await response.Content.ReadAsStringAsync());

                var result = await response.Content.ReadAsStringAsync();

				if (result is null) return new Result(false, "Server is not responding");

				return new Result(true, result);
			}
			catch (Exception e)
			{
				return new Result(false, e.Message);
			}


		}

		public async Task<Result> GetPasteTextAsync(string apiPasteKey)
		{
			try
			{
				var content = new Dictionary<string, string>();

				content.Add("api_dev_key", DEVKEY);
				content.Add("api_user_key", _login.UserKey);
                content.Add("api_option", "show_paste");
				content.Add("api_paste_key", apiPasteKey);

				var response = await _client.PostAsync(new Uri("https://pastebin.com/api/api_post.php"), new FormUrlEncodedContent(content));

				if (response is null) return new Result(false, "Server is not responding");

                if (!response.IsSuccessStatusCode) return new Result(false, await response.Content.ReadAsStringAsync());

                var result = await response.Content.ReadAsStringAsync();

				if (result is null) return new Result(false, "Server is not responding");

				return new Result(true, result);
			}
			catch(Exception e)
			{
				return new Result(false, e.Message);
			}

		}


        public async Task<Result> GetUserInfoAsync()
        {
            try
            {
                var content = new Dictionary<string, string>();

                content.Add("api_dev_key", DEVKEY);
                content.Add("api_user_key", _login.UserKey);
                content.Add("api_option", "userdetails");

                var response = await _client.PostAsync(new Uri("https://pastebin.com/api/api_post.php"), new FormUrlEncodedContent(content));

                if (response is null) return new Result(false, "Server is not responding");

                if (!response.IsSuccessStatusCode) return new Result(false, await response.Content.ReadAsStringAsync());

                var result = await response.Content.ReadAsStringAsync();

                if (result is null) return new Result(false, "Server is not responding");

				var user = Xml.Deserialize<User>(result);

                return new Result(true, user);
            }
            catch (Exception e)
            {
                return new Result(false, e.Message);
            }

		}
	}
}
