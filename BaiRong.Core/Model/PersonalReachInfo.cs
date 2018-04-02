using System;
using BaiRong.Core.Data;


namespace BaiRong.Core.Model
{
   public class PersonalReachInfo
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        public string Mobile { get; set; }
        public string OrganizatioinName { get; set; }
        public string ReachOrganizationName { get; set; }
        public DateTime ReachTime { get; set; }
        public int ActivitiesCount { get; set; }
        public int Integral { get; set; }


        public PersonalReachInfo(object dataItem)
        {
            if (dataItem == null) return;
            ID = SqlUtils.EvalInt(dataItem, "ID");
            UserName = SqlUtils.EvalString(dataItem, "UserName");
            Mobile = SqlUtils.EvalString(dataItem, "Mobile");
            OrganizatioinName = SqlUtils.EvalString(dataItem, "OrganizatioinName");
            ReachOrganizationName = SqlUtils.EvalString(dataItem, "ReachOrganizationName");
            ReachTime = SqlUtils.EvalDateTime(dataItem, "ReachTime");
            ActivitiesCount = SqlUtils.EvalInt(dataItem, "ActivitiesCount");
            Integral = SqlUtils.EvalInt(dataItem, "Integral");
        }


    }
    public class OrganizationReachInfo
    {
        public int ID { get; set; }
        public string AdministratorAccount { get; set; }
        public string TelePhone { get; set; }
        public string OrganizatioinName { get; set; }
        public string ReachOrganizationName { get; set; }
        public DateTime ReachTime { get; set; }
        public int ActivitiesCount { get; set; }
       

        public OrganizationReachInfo(object dataItem)
        {
            if (dataItem == null) return;
            ID = SqlUtils.EvalInt(dataItem, "ID");
            AdministratorAccount = SqlUtils.EvalString(dataItem, "AdministratorAccount");
            TelePhone = SqlUtils.EvalString(dataItem, "TelePhone");
            OrganizatioinName = SqlUtils.EvalString(dataItem, "OrganizationName");
            ReachOrganizationName = SqlUtils.EvalString(dataItem, "ReachOrganizationName");
            ReachTime = SqlUtils.EvalDateTime(dataItem, "ReachTime");
            ActivitiesCount = SqlUtils.EvalInt(dataItem, "ActivitiesCount");
        }

        public OrganizationReachInfo(int id,string name,DateTime reachtime)
        {
            ID = id;
            ReachOrganizationName = name;
            ReachTime = reachtime;
        }

    }
}
