using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;

namespace IOC
{
    public static class Ioc
    {
        private static readonly object LockObj = new object();
        private static UnityContainer container = new UnityContainer();
        public static UnityContainer Container
        {
            get { return container; }
            set
            {
                lock(LockObj){
                    container = value;
                }
            }
        }

        public static T Resolve<T>()
        {
            return container.Resolve<T>();
        }

        public static object Resolve(Type type)
        {
            return container.Resolve(type);
        }
    }
}
