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
                try
                {
                    _secureNum = Security.GetHash(value);
                }
                catch
                {
                    _secureNum = null;
                }
            }
        }

        public string Sno { get; set; }

        public virtual List<Lecture> Lectures { get; set; }

        public int QuestionAnswerd { get; set; }

        public float AnswerScore { get; set; }

        public virtual List<LectureAttendent> lectureAttendent { get; set; }

        private int _ansNum;

        private int _ansCorrect;

        //学生回答问题的总数目
        public int ansNum
        {
            get { return _ansNum; }
            set{
                if(value == 0||value == 1)
                {
                    _ansNum = 2;
                }
                else
                {
                    _ansNum = value;
                }
                CorrectProb = (int)(((float)_ansCorrect / (float)_ansNum) * 100); 
            } }
        //学生回答问题的正确数目
        public int ansCorrect
        {
            get { return _ansCorrect; }
            set{
                if(value == 0)
                {
                    _ansCorrect = 1;
                }
                else
                {
                    _ansCorrect = value;
                }
                CorrectProb = (int)(((float)_ansCorrect / (float)_ansNum) * 100); 
            } 
        }
        //回答正确率
        public int CorrectProb { get; set; }
    }
}
