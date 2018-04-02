using BaiRong.Core.Model.Enumerations;
using BaiRong.Core.Permissions;
using SiteServer.BackgroundPages.Controls;
using SiteServer.BackgroundPages.Core;
using SiteServer.CMS.Core;
using SiteServer.CMS.Core.Security;
using SiteServer.CMS.Core.User;
using SiteServer.CMS.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web.UI.WebControls;
using BaiRong.Core;
using BaiRong.Core.AuxiliaryTable;
using BaiRong.Core.Model;
using BaiRong.Core.Model.Attributes;



namespace SiteServer.BackgroundPages.Cms
{
   public class PageDoubleReach:BasePageCms
    {
        public PlaceHolder phPersonal;
        public PlaceHolder phOrganization;
        public DropDownList ReachCategory;
        public DropDownList SearchType;
        public Repeater rptPersonal;
        public Repeater rpOrganization;
        public SqlPager spContentsPersonal;
        public SqlPager spOrganization;


        public void Page_Load(object sender, EventArgs e)
        {
            if (IsForbidden) return;
            PageUtils.CheckRequestParameter("PublishmentSystemId", "NodeId");
            var PublishementSystemId = Body.GetQueryString("PublishmentSystemId");
            var NodeId = Body.GetQueryInt("NodeId");
            var Type = Body.GetQueryString("Type");           
            if (!IsPostBack)
            {
                try
                {
                    ReachCategory.Items.Add(new ListItem("个人报道", "Personal"));
                    ReachCategory.Items.Add(new ListItem("组织报道", "Organization"));
                    if (string.IsNullOrEmpty(Type)) Type = "Personal";
                    for (int i = 0; i < ReachCategory.Items.Count; i++)
                    {
                        if (ReachCategory.Items[i].Value.Equals(Type))
                        {
                            ReachCategory.Items[i].Selected = true;
                            break;
                        }
                    }
                    if (Type.Equals("Personal"))
                    {
                        phPersonal.Visible = true;
                        phOrganization.Visible = false;
                        spContentsPersonal.ControlToPaginate = rptPersonal;
                        rptPersonal.ItemDataBound += rptPersonal_ItemDataBound;
                        spContentsPersonal.ItemsPerPage = 20;
                        spContentsPersonal.SelectCommand = DataProvider.OrganizationDao.GetSelectCommend("Personal");
                        spContentsPersonal.SortField = DataProvider.OrganizationDao.GetSortFieldName();
                        spContentsPersonal.SortMode = SortMode.DESC;
                        spContentsPersonal.OrderByString = ETaxisTypeUtils.GetOrderByStringForReport();
                        spContentsPersonal.IsQueryTotalCount = true;
                        spContentsPersonal.DataBind();
                        //spContentsPersonal.TotalCount = contentNum;
                    }
                    else if (Type.Equals("Organization"))
                    {
                        phPersonal.Visible = false;
                        phOrganization.Visible = true;
                        spOrganization.ControlToPaginate = rpOrganization;
                        rpOrganization.ItemDataBound += rpOrganization_ItemDataBound;
                        spOrganization.ItemsPerPage = 20;
                        spOrganization.SelectCommand = DataProvider.OrganizationDao.GetSelectCommend("Organization");
                        spOrganization.SortField = DataProvider.OrganizationDao.GetSortFieldName();
                        spOrganization.SortMode = SortMode.ASC;
                        spOrganization.OrderByString = ETaxisTypeUtils.GetOrderByStringForReport();
                        spOrganization.IsQueryTotalCount = true;
                        spOrganization.DataBind();
                    }
                }
                catch(Exception ex)
                {
                    FailMessage(ex.Message);
                }
               
            }

        }
        public void ChannelCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            Response.Redirect(PageUrl, true);
        }
        public void rptPersonal_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var personalReachInfo = new PersonalReachInfo(e.Item.DataItem);
                var ltlPersonalID = (Literal)e.Item.FindControl("ltlPersonalID");
                var ltlPersonalName = (Literal)e.Item.FindControl("ltlPersonalName");
                var ltlPersonalReachCommunity = (Literal)e.Item.FindControl("ltlPersonalReachCommunity");
                var ltlPersonalMobile = (Literal)e.Item.FindControl("ltlPersonalMobile");
                var ltlPersonalCommunity = (Literal)e.Item.FindControl("ltlPersonalCommunity");
                var ltlPersonalReachTime = (Literal)e.Item.FindControl("ltlPersonalReachTime");
                var ltlPersonalActivityCount = (Literal)e.Item.FindControl("ltlPersonalActivityCount");
                var ltlPersonalIntegral = (Literal)e.Item.FindControl("ltlPersonalIntegral");
                var ltlPersonalOperation = (Literal)e.Item.FindControl("ltlPersonalOperation");

                ltlPersonalID.Text = personalReachInfo.ID.ToString();
                ltlPersonalName.Text = personalReachInfo.UserName;
                ltlPersonalMobile.Text = personalReachInfo.Mobile;
                ltlPersonalCommunity.Text = personalReachInfo.OrganizatioinName;
                ltlPersonalReachCommunity.Text = personalReachInfo.ReachOrganizationName;
                ltlPersonalReachTime.Text = personalReachInfo.ReachTime.ToString();
                ltlPersonalActivityCount.Text = personalReachInfo.ActivitiesCount.ToString();
                ltlPersonalIntegral.Text = personalReachInfo.Integral.ToString();
                ltlPersonalOperation.Text = $@"<a href=""javascript:void(0);"" style=""color:red;"">操作</a>";
            }
       }
        public void rpOrganization_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var organizationReachInfo = new OrganizationReachInfo(e.Item.DataItem);
                var ltlOrganizationID = (Literal)e.Item.FindControl("ltlOrganizationID");
                var ltlOrganizationName = (Literal)e.Item.FindControl("ltlOrganizationName");
                var ltlOrganizationChargeName = (Literal)e.Item.FindControl("ltlOrganizationChargeName");
                var ltlOrganizationMobile = (Literal)e.Item.FindControl("ltlOrganizationMobile");
                var ltlOrganizationReachCommunity = (Literal)e.Item.FindControl("ltlOrganizationReachCommunity");
                var ltlOrganizationReachTime = (Literal)e.Item.FindControl("ltlOrganizationReachTime");
                var ltlOrganizationActivityCount = (Literal)e.Item.FindControl("ltlOrganizationActivityCount");

                ltlOrganizationID.Text = organizationReachInfo.ID.ToString();
                ltlOrganizationName.Text = organizationReachInfo.OrganizatioinName;
                ltlOrganizationChargeName.Text = organizationReachInfo.AdministratorAccount;
                ltlOrganizationReachCommunity.Text = organizationReachInfo.ReachOrganizationName;
                ltlOrganizationMobile.Text = organizationReachInfo.TelePhone;
                ltlOrganizationReachTime.Text = organizationReachInfo.ReachTime.ToString();
                ltlOrganizationActivityCount.Text = organizationReachInfo.ActivitiesCount.ToString();
            }
        }
        public string PageUrl
        {
           get
            {
                return  PageUtils.GetCmsUrl(nameof(PageDoubleReach), new NameValueCollection
                    {
                        {"PublishmentSystemID", base.PublishmentSystemId.ToString()},
                        {"NodeID", Body.GetQueryString("NodeId")},
                        {"Type",ReachCategory.SelectedValue }
                    });
            }
        }
    }
}
