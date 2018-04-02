using System;
using System.Web.UI.WebControls;
using BaiRong.Core;
using SiteServer.BackgroundPages.Controls;
using SiteServer.BackgroundPages.Core;
using SiteServer.CMS.Core;
using SiteServer.CMS.Model;

namespace SiteServer.BackgroundPages.Cms
{
    public class PageOrganizationReach : BasePageCms
    {
        public TextBox Keyword;
        public Repeater rptContents;
        public SqlPager spContents;
        public Literal ltlContentButtons;

        public void Page_Load(object sender, EventArgs e)
        {
            var operation = Body.GetQueryString("Operation");
            if (!IsPostBack)
            {
                try
                {
                    if (!string.IsNullOrEmpty(operation) && operation.Equals("delete")) DataProvider.OrganizationDao.DeleteAlreadyReach(PublishmentSystemId);
                    if (DataProvider.OrganizationDao.AlreadyReach(PublishmentSystemId)) PageUtils.Redirect($@"/siteserver/cms/pageReachDetails.aspx?PublishmentSystemId={PublishmentSystemId}");
                    ltlContentButtons.Text = WebUtils.GetContentCommandsForTransform("www.baidu.com", PublishmentSystemId);
                }catch(Exception ex)
                {
                    FailMessage(ex.Message);
                }
               
            }
        }
        public void Search_OnClick(object sender, EventArgs e)
        {
            try
            {
                var keywords = Keyword.Text.Trim();
                spContents.ControlToPaginate = rptContents;
                spContents.ItemsPerPage = 10;
                string sql = string.Empty;
                if (string.IsNullOrEmpty(keywords))
                {
                    sql = $@"select * from siteserver_publishmentsystem where ParentsCount>{PublishmentSystemInfo.ParentsCount}";
                }
                else
                {
                    sql = $@"select * from siteserver_publishmentsystem where ParentsCount>{PublishmentSystemInfo.ParentsCount} and PublishmentSystemName like '%{PageUtils.FilterSqlAndXss(keywords.Trim())}%'";
                }
                spContents.SelectCommand = sql;
                spContents.SortField = BaiRongDataProvider.ContentDao.GetSortPublishmentSystem();
                spContents.SortMode = SortMode.ASC;
                rptContents.ItemDataBound += rptContents_ItemDataBound;
                //spContents.OrderByString = ETaxisTypeUtils.GetOrderByString(tableStyle, ETaxisType.OrderByTaxisDesc); 
                spContents.IsQueryTotalCount = true;
                spContents.DataBind();
            }
            catch(Exception ex)
            {
                FailMessage(ex.Message);
            }
           
        }
        public void Sumbmit_OnClick(object sender, EventArgs e)
        {

        }
        void rptContents_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            var ltlID = e.Item.FindControl("ltlID") as Literal;
            var ltlPublishmentSystemName = e.Item.FindControl("ltlPublishmentSystemName") as Literal;
            var ltlCategory = e.Item.FindControl("ltlCategory") as Literal;
            var ltlAddress = e.Item.FindControl("ltlAddress") as Literal;

            var publishmentSystemInfo = new PublishmentSystemInfo(e.Item.DataItem);

            ltlID.Text = publishmentSystemInfo.PublishmentSystemId.ToString();
            ltlPublishmentSystemName.Text = publishmentSystemInfo.PublishmentSystemName;

        }
    }
}
