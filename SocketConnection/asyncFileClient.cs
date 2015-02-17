using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Core;
using System.IO;

namespace SocketConnection
{
    //用于发送文件
    public class asyncFileClient
    {
        private readonly static ManualResetEvent allDone = new ManualResetEvent(false);
        private string fileName = string.Empty;
        public void SendFile(string from = "127.0.0.1", string to = "127.0.0.1", string fromPort = "10001", string toPort = "10000",string fileName = "")
        {
            this.fileName = fileName;
            var sender = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            sender.BeginConnect(new IPEndPoint(IPAddress.Parse(to), int.Parse(toPort)), new AsyncCallback(connectCallback), sender);
            allDone.WaitOne();

        }

        private void connectCallback(IAsyncResult ar)
        {
            allDone.Set();
            var socketRe = ar.AsyncState as Socket;
            socketRe.EndConnect(ar);
            Socket messageSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            messageSocket.BeginConnect(socketRe.RemoteEndPoint, MessageCallback, messageSocket);
            
            //Console.WriteLine("input the file name");
            //var fileName = Console.ReadLine();

            //发送文件
            socketRe.BeginSendFile(fileName, new AsyncCallback(fileSendCallback), socketRe);
            allDone.WaitOne();
        }

        private void MessageCallback(IAsyncResult ar)
        {
            var socketRe = (Socket)ar.AsyncState;
            socketRe.EndConnect(ar);
            FileInfo f = new FileInfo(fileName);
            //发送文件名,文件大小
            socketRe.BeginSend(fileName+f.Length);
        }

        private void fileSendCallback(IAsyncResult ar)
        {
            var socketRe = ar.AsyncState as Socket;
            socketRe.EndSendFile(ar);
            allDone.Set();
        }
    }
}
