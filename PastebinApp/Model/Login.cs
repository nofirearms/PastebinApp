using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PastebinApp.Model
{
	public class Login
	{
		public string Username { get; set; }
		public string Password { get; set; }
		public string UserKey { get; set; }
		public DateTime LoginDate { get; set; }
	}
}
