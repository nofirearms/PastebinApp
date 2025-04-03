using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PastebinApp.Helpers
{
    public static class Xml
    {

        public static T Deserialize<T>(this string value)
        {
            using (var reader = new StringReader(value))
            {
                return (T)new XmlSerializer(typeof(T)).Deserialize(reader);
            }            
        }
    }
}
