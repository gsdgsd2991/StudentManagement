using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Service
{
    public interface IStudentService:ICrudService<Core.Model.Student>
    {
         void changeSecureNumber(int id, string secureNumber);
    }
}
