using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Model
{
    public class LectureOnce:Entity
    {
        public int LectureID { get; set; }
        public virtual Lecture lecture { get; set; }

        public int StudentId { get; set; }
        public virtual Student student { get; set; }
        public int TeacherId { get; set; }
        public virtual Teacher teacher { get; set; }
        //课程开始时间
        public DateTime StartTime { get; set; }
        //课程结束时间
        public DateTime EndTime { get; set; }
        //课程持续时间
        public TimeSpan LectureLast { get; set; }
        //学生在线时长
        public TimeSpan studentOnlineLast { get; set; }
        //学生课程状态，早退，迟到等，位枚举
        public AttendentType state { get; set; }
    }
}
