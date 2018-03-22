using System;
using BaiRong.Core.Data;

namespace BaiRong.Core.Model
{
   public  class StudyRecord : ExtendedAttributes
    {
        public StudyRecord(object dataItem)
        {
            if (dataItem == null) return;
            Id = SqlUtils.EvalInt(dataItem, "ID");
            UserName = SqlUtils.EvalString(dataItem, "UserName");
            StudyState = SqlUtils.EvalInt(dataItem, "StudyState");
            LastStudyTime = SqlUtils.EvalString(dataItem, "LastStudyTime");            
        }
        public int Id { get; set; }
        public string UserName { get; set; }
        public int StudyState { get; set; }
        public string LastStudyTime { get; set; }
    }
}
