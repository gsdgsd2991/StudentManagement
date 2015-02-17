using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Model;
using Core.Repo;
using Core.Service;
using System.Security.Cryptography;
using PostSharp.Patterns.Contracts;

namespace Service
{
    public class StudentService:CrudService<Student>,IStudentService
    {
        public StudentService(IRepo<Student> repo):base(repo)
        {}

        public void changeSecureNumber(int id, string secureNumber)
        {
            repo.Get(id).SecureNum = secureNumber;
            repo.Save();
        }

        
    }
}
 