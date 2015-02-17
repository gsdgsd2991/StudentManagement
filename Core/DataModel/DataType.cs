using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Model
{
    //socket传递的数据类型
    public enum DataType
    {
          fileNames,//学生请求文件列表
          file,//文件类型,提示发送文件
          instruction,//指令类型
          message,//文字类型
          answerQuestion,//回答问题类型
          raiseHand,//举手
          askQuestion,//提问
          vote//投票
    }
}
