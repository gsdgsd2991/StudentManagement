using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Web.Script.Serialization;

namespace Data
{
    //json序列化方式
    public class JsonSerialization<T>:IDataSerialization<T>
    {
        private static readonly JavaScriptSerializer _serializer = new JavaScriptSerializer();

        public string Serilize(T o)
        {
            return _serializer.Serialize(o);

        }

        public T Deserilize(string str)
        {

            return _serializer.Deserialize<T>(str);
        }
    }
}
