using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Teacher.ViewModel
{
    //用于在提问页面显示问题
    public class QuestionViewModel
    {
        private Core.Model.Question question;

        public QuestionViewModel(Core.Model.Question question)
        {
            this.question = question;
        }
        public int Id { 
            get {
                return question.ID;
                 } 
            set {
                question.ID = value;
            } 
        }

        public string Describe { get; set; }

        public string CorrectPercentage { get; set; }
        public DateTime AddTime { get; set; }
    }
}
