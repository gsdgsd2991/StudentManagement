using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Core;

namespace SocketConnection
{
    //接受socket链接，单例模式
    public class asyncServer
    {
        

        public event EventHandler<ReceivedMessageEventArgs> ReceiveMessageEvent;

        private static ManualResetEvent allDone = new ManualResetEvent(false);
           
        private static bool ReceiveConfirm = true;

        private Socket _sockets;

        public Socket sockets
        {
            get
            {
                return _sockets;
            }           
        }
        
        
        public void StartSignal()
        {
            allDone.Set();
        }
        public void Receive(string from = "127.0.0.1", string to = "127.0.0.1", int fromPort = 10000, int toPort = 10001)
        {

            _sockets = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _sockets.Bind(new IPEndPoint(IPAddress.Parse(from), fromPort));
           
            _sockets.Listen(100);
    
            while (ReceiveConfirm)
            {
                allDone.Reset();
                //Console.WriteLine("start listen");
                StartSignal();
                //等待教师确认接受学生连接
                allDone.WaitOne();
                _sockets.BeginAccept(new AsyncCallback(AcceptConnCallback), _sockets);
 
                allDone.WaitOne();
            }
        }

    

        private void AcceptConnCallback(IAsyncResult ar)
        {
            allDone.Set();
           // Console.WriteLine("connection established");
            var socketRe = (Socket)ar.AsyncState;
            socketRe = socketRe.EndAccept(ar);
            // Console.WriteLine("data received :" + socketRe.Available);
            var data = new byte[10000];
            
            socketRe.BeginReceive(data, 0, 100, SocketFlags.None, new AsyncCallback(ReceiveDataCallback), socketRe);
            allDone.WaitOne();
            Console.WriteLine(Encoding.ASCII.GetChars(data));
            EventHandler<ReceivedMessageEventArgs> handler = ReceiveMessageEvent;
            if(handler != null)
            {
                var e = new ReceivedMessageEventArgs();
                e.message.SenderIP = ((IPEndPoint)socketRe.RemoteEndPoint).Address.Address.ToString();
                e.message.SendTime = DateTime.Now;
                e.message.Data = data;
                handler(this, e);
            }

        }

        private void ReceiveDataCallback(IAsyncResult ar)
        {
            allDone.Set();
            var socketRe = ar.AsyncState as Socket;

           // Console.WriteLine("bytes received :" + socketRe.EndReceive(ar));
        }


    }
}
