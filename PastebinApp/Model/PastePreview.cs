using Newtonsoft.Json;
using PastebinApp.Converters;
using PastebinApp.Model.enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PastebinApp.Model
{
	public class PastePreview
	{
		[JsonProperty(PropertyName = "paste_key")]
		public string Key { get; set; }

		[JsonProperty(PropertyName = "paste_date")]
		[JsonConverter(typeof(JsonTimestampToDateTimeConverter))]
		public DateTime Date { get; set; }

		[JsonProperty(PropertyName = "paste_title")]
		public string Title { get; set; }

		[JsonProperty(PropertyName = "paste_size")]
		public int Size { get; set; }

		[JsonProperty(PropertyName = "paste_expire_date")]
		[JsonConverter(typeof(JsonTimestampToDateTimeConverter))]
		public DateTime ExpireDate { get; set; }

		[JsonProperty(PropertyName = "paste_private")]
		public Privacy IsPrivate { get; set; }

		[JsonProperty(PropertyName = "paste_format_long")]
		public string FormatLong { get; set; }

		[JsonProperty(PropertyName = "paste_format_short")]
		public string FormatShort { get; set; }

		[JsonProperty(PropertyName = "paste_url")]
		public string Url { get; set; }

		[JsonProperty(PropertyName = "paste_hits")]
		public int Hits { get; set; }
	}
}
