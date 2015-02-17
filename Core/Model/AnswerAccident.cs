using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Model
{
    //学生课堂回答问题事件记录
    public class AnswerAccident:Entity
    {
        public int StudentId { get; set; }
        //public virtual Student student { get; set; }
        public int QuestionId { get; set; }
        //问题，本体，如果是老师随机提问的则不需要存在
        public virtual Question question { get; set; }
        //学生本题得分
        public AnswerRight score { get; set; }
        //问题回答时间
        public DateTime AnswerTime { get; set; }
        public int lectureOnceId { get; set; }
        //提出问题的课程
        //public virtual LectureOnceOfStudent lectureOnce { get; set; }
        //是否是随机提问
        public bool isRandom { get; set; }

        
    }
}
