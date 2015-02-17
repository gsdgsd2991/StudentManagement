using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Model;

namespace Core.DataModel
{
    public interface IData
    {
        //发送者Id
        string sender { get; set; }
        //信息类型
        DataType dataType { get; set; }
        //发送时间
        DateTime SendTime { get; set; }

    }
}
