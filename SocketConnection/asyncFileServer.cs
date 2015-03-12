using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SocketConnection
{

    public class BufferSocket
    {
        public string teacher;
        public Socket workSocket;
        public byte[] buffer = new byte[100];
    }
    //文件收取
    public class asyncFileServer
    {
        private static ManualResetEvent allDone = new ManualResetEvent(false);

        private static ManualResetEvent receiveDone = new ManualResetEvent(false);
        private static ManualResetEvent realDataDone = new ManualResetEvent(false);

        private static bool ReceiveConfirm = true;

        private static Socket _sockets;


        public static Dictionary<byte[], FileInfo> fileInfo;

        private static void AcceptConnCallback(IAsyncResult ar)
        {
            allDone.Set();
            // Console.WriteLine("connection established");
            var socketRe = (Socket)ar.AsyncState;
            socketRe = socketRe.EndAccept(ar);
            //allDone.Set();
            // Console.WriteLine("data received :" + socketRe.Available);
            // var data = new byte[100];
            BufferSocket bs = new BufferSocket();
            bs.workSocket = socketRe;
            socketRe.BeginReceive(bs.buffer, 0, bs.buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveDataCallback), bs);
            realDataDone.WaitOne();
            //receiveDone.WaitOne();

            //Console.Write(Encoding.Default.GetString(subData));

        }

        private static void ReceiveDataCallback(IAsyncResult ar)
        {
            realDataDone.Set();
            var socketRe = ar.AsyncState as BufferSocket;

            int bytesRead = socketRe.workSocket.EndReceive(ar);
            if (bytesRead > 0)
            {
               // Console.WriteLine(Encoding.Default.GetString(socketRe.buffer));
                var fileSize = BitConverter.ToInt32(socketRe.buffer, 50);
                var fileName = Encoding.Default.GetString(socketRe.buffer, 0, 50).Replace("\0", "");
                FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate);


                var subData = new byte[10];
                for (int i = 0; i < fileSize / subData.Length; i++)
                {
                    socketRe.workSocket.BeginReceive(subData, 0, subData.Length, SocketFlags.None, new AsyncCallback(ReceiveRealData), socketRe);
                    receiveDone.WaitOne();// realDataDone.WaitOne();
                    fs.Write(subData, 0, subData.Length);
                    fs.Flush();
                    //Console.Write(Encoding.Default.GetString(subData));
                }

                socketRe.workSocket.BeginReceive(subData, 0, fileSize % subData.Length, SocketFlags.None, new AsyncCallback(ReceiveRealData), socketRe);
                receiveDone.WaitOne();// realDataDone.WaitOne();
                fs.Write(subData, 0, fileSize % subData.Length);

                fs.Close();
            }
            realDataDone.Set();
            // receiveDone.Set();
            // Console.WriteLine("bytes received :" + socketRe.EndReceive(ar));
        }

        private static void ReceiveRealData(IAsyncResult ar)
        {
            (ar.AsyncState as BufferSocket).workSocket.EndReceive(ar);

            receiveDone.Set();
        }
    }
}
