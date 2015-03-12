using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataModel
{
   public sealed class AskQuestionType:IData
    {
       //接受者id
       public string receiver { get; set; }
       //问题id
       public string questionId { get; set; }
       //提问时间
       public DateTime askTime { get; set; }


       public string sender
       { get; set; }

       public Model.DataType dataType
       { get; set; }


       public string senderSecureNo
       { get; set; }


       public string senderName
       { get; set; }
    }
}
