using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PastebinApp.Converters
{
	class JsonTimestampToDateTimeConverter : JsonConverter<DateTime>
	{
		public override DateTime ReadJson(JsonReader reader, Type objectType, DateTime existingValue, bool hasExistingValue, JsonSerializer serializer)
		{
			var token = JToken.ReadFrom(reader);
			var timestamp = token.ToObject<int>();

			DateTime datetime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
			return datetime.AddSeconds(timestamp).ToLocalTime();
		}

		public override void WriteJson(JsonWriter writer,  DateTime value, JsonSerializer serializer)
		{
			throw new NotImplementedException();
		}
	}
}
