using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PastebinApp.Model
{
	public	class Result
	{
		public bool IsSuccessful { get; set; }

		public object Response { get; set; }

		public Result(bool isSuccessful, object response)
		{
			IsSuccessful = isSuccessful;
			Response = response;
		}
	}

	
}
