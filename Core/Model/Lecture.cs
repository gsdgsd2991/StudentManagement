using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Model
{
    public class Lecture:Entity
    {
        public string Name { get; set; }

        public virtual List<Student> Students { get; set; }

        //public virtual IEnumerable<Teacher> Teacher { get; set; }
        public virtual Teacher teacher { get; set; }

        public virtual List<LectureAttendent> lestureAttendents { get; set; }

    }
}
