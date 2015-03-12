using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SocketConnection
{
    public class asyncFileClient
    {
        static ManualResetEvent connectDone = new ManualResetEvent(false);
        static ManualResetEvent sendDone = new ManualResetEvent(false);
        static string fileName;
        static void Main(string[] args)
        {
            //Console.WriteLine("发送方");
            fileName = Console.ReadLine();
            var startSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            startSocket.BeginConnect(IPAddress.Parse("127.0.0.1"), 10000, ConnectCallback, startSocket);
            connectDone.WaitOne();
            var preInfo = new byte[100];
            var info = new FileInfo(fileName);

            Array.Copy(Encoding.Default.GetBytes(info.Name), preInfo, info.Name.Length > 50 ? 50 : info.Name.Length);
            var fileSize = BitConverter.GetBytes(info.Length);
            Array.Copy(fileSize, 0, preInfo, 50, fileSize.Length);
            startSocket.BeginSendFile(fileName,
               preInfo,
               null,
               0,
              new AsyncCallback(SendCallback),
               startSocket);
            sendDone.WaitOne();
           // Console.ReadLine();
        }

        private static void ConnectCallback(IAsyncResult ar)
        {
            connectDone.Set();
            var reSocket = ar.AsyncState as Socket;
            reSocket.EndConnect(ar);



        }

        private static void SendCallback(IAsyncResult ar)
        {

            var resocket = ar.AsyncState as Socket;
            resocket.EndSendFile(ar);
            sendDone.Set();
        }
    }
}
