using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace HIMS.API.Utility
{
    public static class Convertor
    {
        public static Dictionary<string, object> ToDictionary(this JObject entity)
        {
            var keyValues = new Dictionary<string, object>();
            foreach (var property in entity.Properties())
            {
                keyValues.Add(property.Name, property.Value);
            }
            return keyValues;
        }

        public static List<Dictionary<string, string>> ToArrayDictionary(this JArray entities)
        {
            var list = new List<Dictionary<string, string>>();

            return list;
        }
    }
}
