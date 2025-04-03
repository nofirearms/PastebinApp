using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PastebinApp.temp
{
	class Class1
	{

		public class Rootobject
		{
			public Pastes pastes { get; set; }
		}

		public class Pastes
		{
			public Paste[] paste { get; set; }
		}

		public class Paste
		{
			public string paste_key { get; set; }
			public string paste_date { get; set; }
			public string paste_title { get; set; }
			public string paste_size { get; set; }
			public string paste_expire_date { get; set; }
			public string paste_private { get; set; }
			public string paste_format_long { get; set; }
			public string paste_format_short { get; set; }
			public string paste_url { get; set; }
			public string paste_hits { get; set; }
		}


        // NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
        public partial class user
        {

            private string user_nameField;

            private string user_format_shortField;

            private string user_expirationField;

            private string user_avatar_urlField;

            private byte user_privateField;

            private string user_websiteField;

            private string user_emailField;

            private string user_locationField;

            private byte user_account_typeField;

            private string[] textField;

            public string user_name
            {
                get
                {
                    return this.user_nameField;
                }
                set
                {
                    this.user_nameField = value;
                }
            }

            /// <remarks/>
            public string user_format_short
            {
                get
                {
                    return this.user_format_shortField;
                }
                set
                {
                    this.user_format_shortField = value;
                }
            }

            /// <remarks/>
            public string user_expiration
            {
                get
                {
                    return this.user_expirationField;
                }
                set
                {
                    this.user_expirationField = value;
                }
            }

            /// <remarks/>
            public string user_avatar_url
            {
                get
                {
                    return this.user_avatar_urlField;
                }
                set
                {
                    this.user_avatar_urlField = value;
                }
            }

            /// <remarks/>
            public byte user_private
            {
                get
                {
                    return this.user_privateField;
                }
                set
                {
                    this.user_privateField = value;
                }
            }

            /// <remarks/>
            public string user_website
            {
                get
                {
                    return this.user_websiteField;
                }
                set
                {
                    this.user_websiteField = value;
                }
            }

            /// <remarks/>
            public string user_email
            {
                get
                {
                    return this.user_emailField;
                }
                set
                {
                    this.user_emailField = value;
                }
            }

            /// <remarks/>
            public string user_location
            {
                get
                {
                    return this.user_locationField;
                }
                set
                {
                    this.user_locationField = value;
                }
            }

            /// <remarks/>
            public byte user_account_type
            {
                get
                {
                    return this.user_account_typeField;
                }
                set
                {
                    this.user_account_typeField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlTextAttribute()]
            public string[] Text
            {
                get
                {
                    return this.textField;
                }
                set
                {
                    this.textField = value;
                }
            }
        }


    }
}
