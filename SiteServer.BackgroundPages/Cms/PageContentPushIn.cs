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
    public class PageContentPushIn : BasePageCms
    {
        public Repeater rptContents;
        public SqlPager spContents;


        public void Page_Load(object sender, EventArgs e)
        {
            if (IsForbidden) return;
            PageUtils.CheckRequestParameter("PublishmentSystemID", "NodeID");
            try
            {
                spContents.ControlToPaginate = rptContents;
                rptContents.ItemDataBound += rptContents_ItemDataBound;
                spContents.ItemsPerPage = 15;
                spContents.SelectCommand = $@"select *from siteserver_push where PushInOrganizationId={PublishmentSystemId} ";
                spContents.SortField = BaiRongDataProvider.ContentDao.GetSortFieldName();
                spContents.SortMode = SortMode.DESC;
                spContents.OrderByString = "BY ID DESC ";
                spContents.IsQueryTotalCount = true;
                spContents.DataBind();
            }
            catch (Exception ex)
            {
                PageUtils.RedirectToErrorPage(ex.Message);
            }

        }
        void rptContents_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var pushOutInfo = new PageContentPushInfo(e.Item.DataItem);
                var ltlID = e.Item.FindControl("ltlID") as Literal;
                var ltlTitle = e.Item.FindControl("ltlTitle") as Literal;
                var ltlCategory = e.Item.FindControl("ltlCategory") as Literal;
                var ltlPushCategory = e.Item.FindControl("ltlPushCategory") as Literal;
                var ltlGoal = e.Item.FindControl("ltlGoal") as Literal;
                var ltlOperationTime = e.Item.FindControl("ltlOperationTime") as Literal;
                var ltlState = e.Item.FindControl("ltlState") as Literal;

                ltlID.Text = pushOutInfo.ContentId.ToString();
                ltlTitle.Text = pushOutInfo.ContentId.ToString();
                ltlCategory.Text = pushOutInfo.ContentId.ToString();
                ltlPushCategory.Text = "上推";
                ltlGoal.Text = pushOutInfo.PushInOrganizationId.ToString();
                ltlOperationTime.Text = pushOutInfo.PushDate.ToString();
                ltlState.Text = pushOutInfo.CheckState;



            }
        }
    }
}
