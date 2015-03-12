using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataModel
{
    public sealed class FileType:IData
    {
        //文件名
        public string fileName { get; set; }
        //文件大小
        public float fileSize { get; set; }

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
