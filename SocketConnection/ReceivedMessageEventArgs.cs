using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Model;

namespace SocketConnection
{
    public class ReceivedMessageEventArgs:EventArgs
    {
        public Message message { get; set; }
    }
}
