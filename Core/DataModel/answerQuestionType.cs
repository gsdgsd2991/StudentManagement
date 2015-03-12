using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataModel
{
    public sealed class answerQuestionType:IData
    {
        public string sender { get; set; }
       

        public Model.DataType dataType
        { get; set; }

        public DateTime SendTime
        { get; set; }


        public string senderSecureNo
        { get; set; }




        public string senderName
        { get; set; }
    }
}
