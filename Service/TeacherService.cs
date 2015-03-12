using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using  Core.Service;
using Core.Model;
using Core.Repo;
using Core.DataModel;
using SocketConnection;
using System.Security.Cryptography;
using System.Web.Script.Serialization;
using System.Configuration;
using PostSharp.Patterns.Contracts;



namespace Service
{
    public class TeacherService:CrudService<Teacher>,ITeacherService
    {
        private new readonly MD5 crypt;

        private new readonly JavaScriptSerializer serializer = new JavaScriptSerializer();

        private new readonly asyncClientTest client;
       // private readonly asyncServer server;
       // private new readonly asyncFileClient fileClient;
       // private Core.Model.Teacher _teacher;
      //  private readonly Dictionary<string, string> mapToIp;
        //private readonly Dictionary<string, string> mapToPort;

        public event EventHandler<FileType> fileReceiveEvent;
        public event EventHandler<RaiseHandType> raiseHandEvent;
        public event EventHandler<answerQuestionType> answerQuestionEvent;
        public event EventHandler<VoteType> voteEvent;
        public event EventHandler<CheckIn> checkInEvent;
        //private new readonly asyncServerTest server;
        public TeacherService(IRepo<Teacher> repo):base(repo)
        {
            //server.ReceiveMessageEvent += ReceiveMessage;
            asyncServer.ReceiveMessageEvent += ReceiveMessage;
        }

        public void StartReceive()
        {
            asyncServer.ReceiveConfirm = true;
            //asyncServer.Receive();
            var t = Task.Run(
                () =>
                {
                    asyncServer.Receive();
                }
                );
           // t.Wait();
        }

        public void EndReceive()
        {
            asyncServer.ReceiveConfirm = false;
        }

        public void ChangePassword(int id,string password)
        {
            repo.Get(id).Password = password;
            repo.Save();
        }


        public void SendMessage(IMessage message)
        {
            var jsonMessage = serializer.Serialize(message);
            var fromIpAddress = ConfigurationSettings.AppSettings.GetValues("FromIpAddress")[0];
            var fromPort = ConfigurationSettings.AppSettings.GetValues("FromPort")[0];
            //var toPort = ConfigurationSettings.AppSettings.GetValues("ToPort")[0];
            //client.Send(fromIpAddress, message.Receiver, fromPort, mapToPort[message.Receiver],Encoding.ASCII.GetBytes(jsonMessage));
            //throw new NotImplementedException();
        }
        //订阅事件
        public void ReceiveMessage(object sender,ReceivedMessageEventArgs e)
        {
           // var serializer = new JavaScriptSerializer();
            var data = serializer.Deserialize<IData>(Encoding.Default.GetString( e.message.Data).Replace("\0",""));
            switch(data.dataType)
            {
                 //学生申请文件列表

                //学生提交文件
                case DataType.file:
                    //fileReceiveEvent(this, serializer.Deserialize<FileType>(Encoding.Default.GetString(e.message.Data).Replace("\0","")));
                    break;
                //学生举手
                case DataType.raiseHand:
                    var obj = serializer.Deserialize<RaiseHandType>(Encoding.Default.GetString(e.message.Data).Replace("\0", ""));
                    raiseHandEvent(this, obj);
                    break;
                //学生回答问题
                case DataType.answerQuestion:
                    answerQuestionEvent(this, serializer.Deserialize<answerQuestionType>(Encoding.Default.GetString(e.message.Data).Replace("\0", "")));
                    break;
                //学生投票
                case DataType.vote:
                    voteEvent(this, serializer.Deserialize<VoteType>(Encoding.Default.GetString(e.message.Data).Replace("\0", "")));
                    break;
                case DataType.checkIn:
                    checkInEvent(this,serializer.Deserialize<CheckIn>(Encoding.Default.GetString(e.message.Data).Replace("\0", "")));
                    break;
            }
        }
       
        //订阅事件
        public void ReceiveFile(object sender)
        {
            
        }

        public void SendFile(string receiver,string fileName)
        {
            var fromIpAddress = ConfigurationSettings.AppSettings.GetValues("FromIpAddress")[0];
            var fromPort = ConfigurationSettings.AppSettings.GetValues("FromPort")[0];
            //var toPort = ConfigurationSettings.AppSettings.GetValues("ToPort")[0];
            //fileClient.SendFile(fromIpAddress, mapToIp[receiver], fromPort, mapToPort[receiver], fileName);
        }


        public void ReceiveMessage(IMessage message)
        {
            throw new NotImplementedException();
        }


        public Teacher Get(string userName, string password)
        {
            return repo.where(o=>o.Name == userName&&o.Password == password,false).FirstOrDefault();
        }

       
    }
}
