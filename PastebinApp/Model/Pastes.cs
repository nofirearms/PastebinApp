using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PastebinApp.Model
{
	public class Pastes
	{
		[JsonProperty(PropertyName = "paste")]
		public PastePreview[] PastesList { get; set; }
	}
	//костыль для сериализации
	public class PastesRoot
	{
		[JsonProperty(PropertyName = "pastes")]
		public Pastes Pastes { get; set; }
	}
}
