using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SocketConnection
{
    public class asyncFileServer
    {
        private readonly new ManualResetEvent allDone;
        public void Receive(string from="127.0.0.1",string to="127.0.0.1",int fromPort = 10001,int toPort = 20001)
        {
            var sockets = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        }
    }
}
