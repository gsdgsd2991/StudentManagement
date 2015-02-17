using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Core.Model
{
    public class Student:Entity
    {
        public string Name { get; set; }

        private string _secureNum;

        public string SecureNum {
            get
            {
                return _secureNum;
            }
            set
            {
                _secureNum = Security.GetHash(value);
            }
        }

        public string Sno { get; set; }

        public virtual List<Lecture> Lectures { get; set; }

        public int QuestionAnswerd { get; set; }

        public float AnswerScore { get; set; }
    }
}
