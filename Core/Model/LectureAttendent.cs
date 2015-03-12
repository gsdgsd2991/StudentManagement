using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Model
{
    public class LectureAttendent:Entity
    {
        public int StudentID { get; set; }

        public int LectureID { get; set; }

        public int TeacherID { get; set; }
        //课程开始时间
        public DateTime LectureStart { get; set; }
        //课程结束时间
        public DateTime LectureEnd { get; set; }
        //学生在线时间
        public DateTime LectureLast { get; set; }

        public virtual Lecture lecture { get; set; }

        public virtual Teacher teacher { get; set; }

        public virtual Student student { get; set; }
        
    }
}
