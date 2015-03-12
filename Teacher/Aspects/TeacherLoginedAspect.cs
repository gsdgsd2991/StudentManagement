using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PostSharp;
using PostSharp.Aspects;

namespace Teacher.Aspects
{
    //教师没有登陆
    [Serializable]
    public class TeacherLoginedAttribute : OnMethodBoundaryAspect 
    {
        //跳过函数执行，抛出异常
        public override void OnEntry(MethodExecutionArgs args)
        {
            if(LoginStatus.Teacher == null||LoginStatus.TeacherHasLogin == false)
            {
               args.FlowBehavior = FlowBehavior.Return;
               throw new TeacherNotLoginedException();
            }
          
        }

        
    }

    public class TeacherNotLoginedException:Exception
    {

    }
}
