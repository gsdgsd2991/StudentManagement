using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using Core.Model;

namespace Data
{
    //字符串序列化方式实现
    public interface IDataSerialization<T>
    {
        string Serilize(T o);
        T Deserilize(string str);
    }
}
