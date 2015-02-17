using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Threading;
using Core;

namespace SocketConnection
{
    //用于发送消息
    public class asyncClientTest
    {
        private readonly static ManualResetEvent allDone = new ManualResetEvent(false);
        public void Send(string from = "127.0.0.1", string to = "127.0.0.1", string fromPort = "20000", string toPort = "10000",byte[] data = null)
        {
            var sockets = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

         
            sockets.BeginConnect(new IPEndPoint(IPAddress.Parse(to), int.Parse(toPort)), new AsyncCallback(ConnectedCallback), sockets);
            allDone.WaitOne();
            //var data = Encoding.ASCII.GetBytes("hello from client,time is:" + DateTime.Now + "<EOF>");
            sockets.BeginSend(data, 0, data.Length, SocketFlags.None, new AsyncCallback(SendCallback), sockets);
            allDone.WaitOne();
        }

        private void SendCallback(IAsyncResult ar)
        {
            var sockets = ar.AsyncState as Socket;

//            Console.WriteLine("send success:" + sockets.EndSend(ar));
            allDone.Set();
        }

        private void ConnectedCallback(IAsyncResult ar)
        {
            var socketRe = ar.AsyncState as Socket;
            socketRe.EndConnect(ar);
            allDone.Set();
         }




    }
}
