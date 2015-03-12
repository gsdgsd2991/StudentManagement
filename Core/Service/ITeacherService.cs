using Core.DataModel;
using Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Core.Service
{
    public interface ITeacherService:ICrudService<Teacher>
    {
        //更改密码
        void ChangePassword(int id,string password);

        void SendMessage(IMessage message);

        void ReceiveMessage(IMessage message);

        Teacher Get(string userName, string password);
         void StartReceive();
         void EndReceive();
         event EventHandler<FileType> fileReceiveEvent;
         event EventHandler<RaiseHandType> raiseHandEvent;
         event EventHandler<answerQuestionType> answerQuestionEvent;
         event EventHandler<VoteType> voteEvent;
         event EventHandler<CheckIn> checkInEvent;
    }
}
