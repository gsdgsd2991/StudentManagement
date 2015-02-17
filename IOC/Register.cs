using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;

namespace IOC
{
    public class Registers
    {
        

        public static void Register(Type interfaceType,Type implementType)
        {
            Ioc.Container.RegisterType(interfaceType, implementType);
        }

      
    }
}
