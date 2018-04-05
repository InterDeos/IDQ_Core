using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;

namespace IDQ_Core_0.Class
{
    public static class JSONSerializer
    {
        public static string Serialize(this object obj)
        {
            var jss = new JavaScriptSerializer();
            return jss.Serialize(obj);
        }
        public static T Deserialize<T>(string jsonstring)
        {
            var jss = new JavaScriptSerializer();
            return jss.Deserialize<T>(jsonstring);
        }
        
        public static void SerializeToFile(this object obj, string path)
        {
            FileManager.WriteTextInFile(obj.Serialize(), path);
        }
        public static T DeserializeFromFile<T>(string path)
        {
            return Deserialize<T>(FileManager.ReadTextFromFile(path));
        }
    }
}
