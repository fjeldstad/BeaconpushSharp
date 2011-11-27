using System;
using System.Web.Script.Serialization;

namespace BeaconpushSharp.Core
{
    public class DefaultJsonSerializer : IJsonSerializer
    {
        public string Serialize(object data)
        {
            var serializer = new JavaScriptSerializer();
            return serializer.Serialize(data);
        }

        public T Deserialize<T>(string data)
        {
            var serializer = new JavaScriptSerializer();
            return serializer.Deserialize<T>(data);
        }
    }
}