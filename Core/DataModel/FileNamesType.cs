using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataModel
{
    public class FileNamesType:IData
    {
        public string sender
        {
            get;
            set;
        }

        public Model.DataType dataType
        {
            get;
            set;
        }

        public DateTime SendTime
        {
            get;
            set;

        }
        
        string TeacherList { get; set; }

        string FileList { get; set; }
    }
}
