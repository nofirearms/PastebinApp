using System.Xml.Serialization;

namespace PastebinApp.Model
{
    [XmlRoot("user")]
    public class User
    {

        [XmlElement("user_name")]
        public string Name { get; set; }
        [XmlElement("user_format_short")]
        public string Format { get; set; }
        [XmlElement("user_expiration")]
        public string Expiration { get; set; }
        
        private string _avatarUrl;
        [XmlElement("user_avatar_url")]
        public string AvatarUrl
        {
            get => $"https://pastebin.com{_avatarUrl}"; 
            set => _avatarUrl = value;
        }
        [XmlElement("user_private")]
        public byte Private { get; set; }
        [XmlElement("user_website")]
        public string Website { get; set; }
        [XmlElement("user_email")]
        public string Email { get; set; }
        [XmlElement("user_location")]
        public string Localization { get; set; }
        [XmlElement("user_account_type")]
        public byte AccountType { get; set; }
    }
}
