using System;
using BaiRong.Core.Data;

namespace BaiRong.Core.Model
{
    public class PartyTransCheckInfo
    {
        public PartyTransCheckInfo()
        {
            ID = 0;
            ApplyUserId = 0;
            UserName = string.Empty;
            MobilePhone = string.Empty;
            TransformOutId = 0;
            TransformOutName = string.Empty;
            TransformInId = 0;
            TransformInName = string.Empty;
            ReachOrganizationId = 0;
            ApplyDate = DateUtils.SqlMinValue;
            OutCheckState = 0;
            OutCheckStateString = string.Empty;
            InCheckState = 0;
            InCheckStateString = string.Empty;
            InCheckDate = DateUtils.SqlMinValue;
        }

        public PartyTransCheckInfo(object dataItem)
        {
            if (dataItem == null) return;
            ID = SqlUtils.EvalInt(dataItem, "ID");
            ApplyUserId = SqlUtils.EvalInt(dataItem, "ApplyUserId");
            UserName = SqlUtils.EvalString(dataItem, "UserName");
            MobilePhone= SqlUtils.EvalString(dataItem, "MobilePhone");
            TransformOutId = SqlUtils.EvalInt(dataItem, "TransformOutId");
            TransformOutName = SqlUtils.EvalString(dataItem, "TransformOutName");
            TransformInId = SqlUtils.EvalInt(dataItem, "TransformInId");
            TransformInName = SqlUtils.EvalString(dataItem, "TransformInName");
            ReachOrganizationId = SqlUtils.EvalInt(dataItem, "ReachOrganizationId");
            ApplyDate = SqlUtils.EvalDateTime(dataItem, "ApplyDate");
            OutCheckState = SqlUtils.EvalInt(dataItem, "OutCheckState");
            InCheckState = SqlUtils.EvalInt(dataItem, "InCheckState");
            InCheckDate = SqlUtils.EvalDateTime(dataItem, "InCheckDate");
            OutCheckStateString = OutCheckState == 0 ? "δ���" : (OutCheckState == 1 ? "���ͨ��" : "���δͨ��");
            InCheckStateString = InCheckState == 0 ? "δ���" : (InCheckState == 1 ? "���ͨ��" : "���δͨ��");
        }

        public int ID { get; set; }

        public int ApplyUserId { get; set; }

        public string UserName { get; set; }

        public string MobilePhone { get; set; }

        public int TransformOutId { get; set; }

        public string TransformOutName { get; set; }

        public int TransformInId { get; set; }
        public string TransformInName { get; set; }

        public int ReachOrganizationId { get; set; }

        public DateTime ApplyDate { get; set; }

        public int OutCheckState { get; set; }
        public string OutCheckStateString { get; set; }

        public DateTime OutCheckDate { get; set; }

        public int InCheckState { get; set; }
        public string InCheckStateString { get; set; }
        public DateTime InCheckDate { get; set; }
    }
}
