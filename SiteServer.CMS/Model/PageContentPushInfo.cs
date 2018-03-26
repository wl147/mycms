using BaiRong.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteServer.CMS.Model
{
   public class PageContentPushInfo
    {
        public int ContentId { get; set; }
        public int NodeId { get; set; }
        public string ContentType { get; set; }
        public string PushType { get; set; }
        public int PushInOrganizationId { get; set; }
        public int PushOutOrganizationId { get; set; }
        public string PushOutCheck { get; set; }
        public string PushInCheck { get; set; }
        public DateTime PushDate { get; set; }
        public DateTime PushOutCheckTime { get; set; }
        public DateTime PushInCheckTime { get; set; }
        public string CheckState { get; set; }
        public PageContentPushInfo()
        {
            ContentId = 0;
            NodeId = 0;
            ContentType = string.Empty;
            PushType = string.Empty;
            PushInOrganizationId = 0;
            PushOutOrganizationId = 0;
            PushOutCheck = string.Empty;
            PushInCheck = string.Empty;
            PushDate = DateTime.MinValue;
            PushOutCheckTime = DateTime.MinValue;
            PushInCheckTime = DateTime.MinValue;
            CheckState = string.Empty;
        }
        public PageContentPushInfo(object dataItem)
        {
            if (dataItem == null) return;
            ContentId = SqlUtils.EvalInt(dataItem, "ContentId");
            NodeId = SqlUtils.EvalInt(dataItem, "NodeId");
            ContentType = SqlUtils.EvalString(dataItem, "ContentType");
            PushType = SqlUtils.EvalString(dataItem, "ContentType");
            PushInOrganizationId = SqlUtils.EvalInt(dataItem, "PushInOrganizationId"); ;
            PushOutOrganizationId = SqlUtils.EvalInt(dataItem, "PushOutOrganizationId"); ;
            PushOutCheck = SqlUtils.EvalString(dataItem, "PushOutCheck");
            PushInCheck = SqlUtils.EvalString(dataItem, "PushInCheck");
            PushDate = SqlUtils.EvalDateTime(dataItem, "PushDate");
            PushOutCheckTime = SqlUtils.EvalDateTime(dataItem, "PushOutCheckTime");
            PushInCheckTime = SqlUtils.EvalDateTime(dataItem, "PushInCheckTime");
            CheckState=SqlUtils.EvalString(dataItem, "CheckState");
        }
    }
}
