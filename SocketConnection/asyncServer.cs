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
    public static class asyncServer
    {
        

        public static event EventHandler<ReceivedMessageEventArgs> ReceiveMessageEvent;

        private static ManualResetEvent allDone = new ManualResetEvent(false);
        private static ManualResetEvent receiveDone = new ManualResetEvent(false);
        public static bool ReceiveConfirm = true;

        private static Socket _sockets;

        public static Socket sockets
        {
            get
            {
                return _sockets;
            }           
        }
        
        
       // public static void StartSignal()
        //{
         //   allDone.Set();
        //}
        public static void Receive(string from = "127.0.0.1", string to = "127.0.0.1", int fromPort = 10000, int toPort = 10001)
        {

            _sockets = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _sockets.Bind(new IPEndPoint(IPAddress.Parse(from), fromPort));
           
            _sockets.Listen(100);
    
            while (ReceiveConfirm)
            {
                allDone.Reset();
                //Console.WriteLine("start listen");
          //      StartSignal();
                //等待教师确认接受学生连接
                
                    _sockets.BeginAccept(new AsyncCallback(AcceptConnCallback), _sockets);
 
                allDone.WaitOne();
            }
        }

    

        private static void AcceptConnCallback(IAsyncResult ar)
        {
            allDone.Set();
           // Console.WriteLine("connection established");
            var socketRe = (Socket)ar.AsyncState;
            socketRe = socketRe.EndAccept(ar);
            // Console.WriteLine("data received :" + socketRe.Available);
            var data = new byte[100];
            
            socketRe.BeginReceive(data, 0, data.Length, SocketFlags.None, new AsyncCallback(ReceiveDataCallback), socketRe);
            receiveDone.WaitOne();// allDone.WaitOne();
            //Console.WriteLine(Encoding.Default.GetChars(data));
            EventHandler<ReceivedMessageEventArgs> handler = ReceiveMessageEvent;
            //if(handler != null)
            //{
                var e = new ReceivedMessageEventArgs();
                e.message = new Core.Model.Message();
                e.message.SenderIP = ((IPEndPoint)socketRe.RemoteEndPoint).Address.Address.ToString();
                e.message.SendTime = DateTime.Now;
                e.message.Data = data;
                handler(null, e);
            //}

        }

        private static void ReceiveDataCallback(IAsyncResult ar)
        {
            receiveDone.Set();//allDone.Set();
            var socketRe = ar.AsyncState as Socket;

           // Console.WriteLine("bytes received :" + socketRe.EndReceive(ar));
        }


    }
}
