using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Model
{
    //学生登陆的几种状态,位枚举，各种状态可以同时存在
    //[Flags]
    public enum AttendentType
    {
        //迟到
        Late,
        //早退
        LeaveEarly,
        //自动签到
        Attend,
        //手机故障或者没有智能手机等，老师手动签到
        AttendByTeacher
    }
    //学生回答问题得分
    public enum AnswerRight
    {
        A,
        B,
        C,
        D,
        E
    }
}
