using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Model;

namespace Core.DataModel
{
    public class IData
    {
        //发送者Id
       public string sender { get; set; }
        //发送者安全码
       public string senderSecureNo { get; set; }
        //发送者姓名
       public string senderName { get; set; }
        //信息类型
      public  DataType dataType { get; set; }
       

    }
}
