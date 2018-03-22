using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web.UI.WebControls;
using BaiRong.Core;
using BaiRong.Core.AuxiliaryTable;
using BaiRong.Core.Model;
using BaiRong.Core.Model.Attributes;
using BaiRong.Core.Model.Enumerations;
using BaiRong.Core.Permissions;
using SiteServer.BackgroundPages.Controls;
using SiteServer.BackgroundPages.Core;
using SiteServer.CMS.Core;
using SiteServer.CMS.Core.Security;
using SiteServer.CMS.Core.User;
using SiteServer.CMS.Model;

namespace SiteServer.BackgroundPages.Cms
{
    public class PageStudyRecord : BasePageCms
    {


        public Repeater RptContents;
        public SqlPager SpContents;      


        public static string GetRedirectUrl(int publishmentSystemId)
        {
            return PageUtils.GetCmsUrl(nameof(PageContentCheck), new NameValueCollection
            {
                {"PublishmentSystemID", publishmentSystemId.ToString()}
            });
        }

        public void Page_Load(object sender, EventArgs e)
        {
            if (IsForbidden) return;

            PageUtils.CheckRequestParameter("ArticleId");
            int ArticleId = Body.GetQueryInt("ArticleId");
            
            if (!IsPostBack)
            {
                SpContents.ControlToPaginate = RptContents;
                SpContents.ItemsPerPage = 15;
                RptContents.ItemDataBound += rptContents_ItemDataBound;
                SpContents.SelectCommand =$@"SELECT a.UserName,b.ID,b.SdudyState,b.LastStudyTime from bairong_users as a,siteserver_userinstudy as b where a.UserId=b.UserID and b.StudyID={ArticleId}";

                SpContents.SortField = ContentAttribute.Id;
                SpContents.SortMode = SortMode.DESC;
                RptContents.ItemDataBound += rptContents_ItemDataBound;

                SpContents.DataBind();

            }
        }

        void rptContents_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var tbId = (Literal)e.Item.FindControl("tbId");
                var tbName = (Literal)e.Item.FindControl("tbName");
                var tbState = (Literal)e.Item.FindControl("tbState");
                var tbLastStudyTime = (Literal)e.Item.FindControl("tbLastStudyTime");
                var studyInfo = new StudyRecord(e.Item.DataItem);
                tbId.Text = studyInfo.Id.ToString();
                tbName.Text = studyInfo.UserName;
                tbState.Text = studyInfo.StudyState==0?"未完成":"已完成";
                tbLastStudyTime.Text = studyInfo.LastStudyTime.ToString();



            }
        }

       

        
    }
}
