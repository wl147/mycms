using System;
using System.Web.UI.WebControls;
using BaiRong.Core;
using SiteServer.CMS.Core;


namespace SiteServer.BackgroundPages.Cms
{
    public class PageReachDetails : BasePageCms
    {
        public Literal ltlCommunity;
        public Literal ltlContentButtons;
        public Literal ltlTime;
        public void Page_Load(object sender, EventArgs e)
        {
            
            if (!IsPostBack)
            {
                try
                {
                    if (!DataProvider.OrganizationDao.AlreadyReach(PublishmentSystemId)) PageUtils.Redirect($@"/siteserver/cms/PageOrganizationReach.aspx?PublishmentSystemId={PublishmentSystemId}");
                    var reachInfo = DataProvider.OrganizationDao.GetReachInfoDetails(PublishmentSystemId);
                    ltlCommunity.Text = $@"<span style=""font-size:20px;color:blue;border:1px; "">{reachInfo.ReachOrganizationName} </span>";
                    ltlContentButtons.Text = $@"<a style=""font-size:20px;color:red;border:1px;"" href=""/siteserver/cms/pageOrganizationReach.aspx?PublishmentSystemID={PublishmentSystemId}&Operation=delete"" "">取消报道</a>";
                    ltlTime.Text = $@"<span style=""font-size:16px;color:red;border:1px; "">{reachInfo.ReachTime} </span>";
                }
                catch(Exception ex)
                {
                    FailMessage(ex.Message);
                }        
            }
        }
    }
}
