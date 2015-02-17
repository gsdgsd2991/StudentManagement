using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;


namespace Core.Model
{
    public class Teacher:Entity
    {

        public string Name { get; set; }

        public string _password;

      
        public string Password 
        {
            get 
            {
                return _password;
            }
            set
            {
                _password = Security.GetHash(value);
            }
        }

        public virtual IEnumerable<Lecture> Lectures { get; set; }
    }
}
