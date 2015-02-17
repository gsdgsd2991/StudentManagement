using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Model
{
    public class Message:IMessage
    {
        public Message(string sender,string senderIp,string receiver,string receiverIp,DateTime sendTime,byte[] data)
        {
            this.Sender = sender;
            this.SenderIP = SenderIP;
            this.Receiver = receiver;
            this.ReceiverIP = receiverIp;
            this.SendTime = sendTime;
            this.Data = data;
            
        }

        public Message()
        {
            // TODO: Complete member initialization
        }
        public string SenderIP{get;set;}//ip

        public string ReceiverIP{get;set;}//ip

        public DateTime SendTime { get; set; }

        public byte[] Data { get; set; }//json

        public string Sender
        {
            get;
            set;
        }

        public string Receiver
        {
            get;
            set;
        }
    }
}
