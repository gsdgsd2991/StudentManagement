using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;


namespace Core.Model
{
    public enum QuestionType
    {
        vote,answer
    }

    public class Question:Entity
    {
       /* [Key]
        public int Id { get; set; }*/
        //问题描述
        public string QuestionDescribe { get; set; }
        //答案描述
        public string AnswerDescribe { get; set; }
        //题目类型
      //  public QuestionType _QuestionType { get; set; }
        //问题所属课程Id
        public int LectureId { get; set; }
        //是否是抢答
       // public bool IsGrab { get; set; }
        //创建问题教师Id
        public string TeacherId { get; set; }
        //问题正确率
        public float CorrectPercentage { get; set; }
        //问题添加时间
        public DateTime AddTime { get; set; }
        //创建人
        public virtual Teacher CreatedBy { get; set; }
        //问题所属课程
        public virtual Lecture LectureBelonged { get; set; }
    }
}
