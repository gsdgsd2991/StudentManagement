﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataModel
{
    public class VoteType:IData
    {
        public int selection { get; set; }



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
